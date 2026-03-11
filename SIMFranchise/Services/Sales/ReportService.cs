using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Reports;
using SIMFranchise.DTOs.Sales;
using SIMFranchise.Interfaces; 

namespace SIMFranchise.Services.Sales

{
    public class ReportService : IReportService
    {
        private readonly SimfranchiseManagementDbContext _context;

        public ReportService(SimfranchiseManagementDbContext context)
        {
            _context = context;
        }

        // 1. Page 1: Franchise ki Grand Summary
        public async Task<FranchiseGrandReportDto> GetFranchiseGrandReportAsync(int franchiseId, string filterType)
        {
            var result = await _context.Database
                .SqlQueryRaw<FranchiseGrandReportDto>(
                    "EXEC [dbo].[sp_GetFranchiseGrandReport] @FranchiseId = {0}, @FilterType = {1}",
                    franchiseId,
                    filterType.ToUpper()
                )
                .ToListAsync();

            return result.FirstOrDefault() ?? new FranchiseGrandReportDto();
        }

        // 2. Page 2: Member-wise Sales Breakdown
        public async Task<List<MemberSaleReportDto>> GetMemberSaleReportAsync(int franchiseId, string filterType)
        {
            var result = await _context.Database
                .SqlQueryRaw<MemberSaleReportDto>(
                    "EXEC [dbo].[sp_GetMemberSaleReport] @FranchiseId = {0}, @FilterType = {1}",
                    franchiseId,
                    filterType.ToUpper()
                )
                .ToListAsync();

            return result;
        }
    }
}