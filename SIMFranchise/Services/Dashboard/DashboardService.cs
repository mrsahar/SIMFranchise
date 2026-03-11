using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Dashboard;
using SIMFranchise.Interfaces;
using SIMFranchise.Interfaces.Dashboard;

namespace SIMFranchise.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly SimfranchiseManagementDbContext _context;

        public DashboardService(SimfranchiseManagementDbContext context)
        {
            _context = context;
        }

        public async Task<FranchiseDashboardDto> GetFranchiseDashboardAsync(int franchiseId)
        {
            try
            {
                // SP Call: sp_GetFranchiseDashboard @FranchiseId
                // Ye ek hi round-trip mein saara data le aayega
                var data = await _context.Database
                    .SqlQueryRaw<FranchiseDashboardDto>(
                        "EXEC [dbo].[sp_GetFranchiseDashboard] @FranchiseId = {0}",
                        franchiseId
                    )
                    .ToListAsync();

                return data.FirstOrDefault() ?? new FranchiseDashboardDto();
            }
            catch (Exception ex)
            {
                // Log error here
                throw new Exception("Dashboard data fetch error: " + ex.Message);
            }
        }
    }
}