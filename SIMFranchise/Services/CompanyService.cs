using Microsoft.EntityFrameworkCore;
using SIMFranchise.Models; // DbContext ke liye
using SIMFranchise.DTOs.Company;
using SIMFranchise.Interfaces;
using AutoMapper;
using SIMFranchise.Data;

namespace SIMFranchise.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly SimfranchiseManagementDbContext _context;
        private readonly IMapper _mapper;

        // Constructor: Hum DbContext aur AutoMapper ko yahan inject kar rahe hain
        public CompanyService(SimfranchiseManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // 1. Saari Companies hasil karne ke liye
        public async Task<List<CompanyResponseDto>> GetAllCompaniesAsync()
        {
            // Database se saara data list ki surat mein nikaalte hain
            var companies = await _context.Companies.ToListAsync();

            // Domain Model ki list ko Response DTO ki list mein convert karte hain
            return _mapper.Map<List<CompanyResponseDto>>(companies);
        }

        // 2. ID ke zariye kisi ek Company ko dhundne ke liye
        public async Task<CompanyResponseDto?> GetCompanyByIdAsync(int id)
        {
            // FindAsync ID ke base par record search karta hai
            var company = await _context.Companies.FindAsync(id);

            if (company == null) return null;

            // Agar record mil jaye to usay DTO mein convert kar ke bhejte hain
            return _mapper.Map<CompanyResponseDto>(company);
        }

        // 3. Nayi Company create karne ke liye
        public async Task<CompanyResponseDto> CreateCompanyAsync(CompanyCreateDto dto)
        {
            // User se aaye huye DTO ko Database Model (Entity) mein map karte hain
            var company = _mapper.Map<Company>(dto);

            // Database context mein add karte hain
            await _context.Companies.AddAsync(company);

            // Asliyat mein database mein save karte hain
            await _context.SaveChangesAsync();

            // Save hone ke baad bani hui company ko wapis Response DTO mein bhejte hain (Id ke saath)
            return _mapper.Map<CompanyResponseDto>(company);
        }

        // 4. Maujooda Company ko update karne ke liye
        public async Task<bool> UpdateCompanyAsync(int id, CompanyUpdateDto dto)
        {
            // Pehle check karte hain ke ye ID database mein maujood hai ya nahi
            var existingCompany = await _context.Companies.FindAsync(id);

            if (existingCompany == null)
            {
                return false; // Record nahi mila
            }

            // DTO ki nayi values ko existing record ke upar copy/map karte hain
            _mapper.Map(dto, existingCompany);

            // Changes ko save karte hain
            await _context.SaveChangesAsync();

            return true;
        }
    }
}