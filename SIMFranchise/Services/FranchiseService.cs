using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Franchise;
using SIMFranchise.DTOs.Franchise.SIMFranchise.DTOs.Franchise;
using SIMFranchise.Interfaces;
using SIMFranchise.Models;

namespace SIMFranchise.Services
{
    public class FranchiseService : IFranchiseService
    {
        private readonly SimfranchiseManagementDbContext _context;
        private readonly IMapper _mapper;

        public FranchiseService(SimfranchiseManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // 1. Saari Franchises hasil karna (With Company Name)
        public async Task<List<FranchiseResponseDto>> GetAllFranchisesAsync()
        {
            // .Include(f => f.Company) ka matlab hai ke Franchise ke saath uski Company ka data bhi lao
            var franchises = await _context.Franchises
                .Include(f => f.Company)
                .ToListAsync();

            return _mapper.Map<List<FranchiseResponseDto>>(franchises);
        }

        // 2. ID se dhundna
        public async Task<FranchiseResponseDto?> GetFranchiseByIdAsync(int id)
        {
            var franchise = await _context.Franchises
                .Include(f => f.Company)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (franchise == null) return null;

            return _mapper.Map<FranchiseResponseDto>(franchise);
        }

        // 3. Nayi Franchise banana
        public async Task<FranchiseResponseDto> CreateFranchiseAsync(FranchiseCreateDto dto)
        {
            var franchise = _mapper.Map<Franchise>(dto);

            // CreatedDate manually set kar dete hain agar DB default nahi hai
            franchise.CreatedDate = DateTime.Now;

            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();

            // Wapis bhejte waqt Company ka data bhi chahiye ho sakta hai
            return await GetFranchiseByIdAsync(franchise.Id);
        }

        // 4. Update karna
        public async Task<bool> UpdateFranchiseAsync(int id, FranchiseUpdateDto dto)
        {
            var existingFranchise = await _context.Franchises.FindAsync(id);
            if (existingFranchise == null) return false;

            _mapper.Map(dto, existingFranchise);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}