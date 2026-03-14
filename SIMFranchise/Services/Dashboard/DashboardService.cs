using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Dashboard;
using SIMFranchise.Interfaces;
using SIMFranchise.Interfaces.Dashboard;
using System.Data;

namespace SIMFranchise.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly SimfranchiseManagementDbContext _context;
        private readonly string _connectionString;

        public DashboardService(SimfranchiseManagementDbContext context)
        {
            _context = context;
            _connectionString = _context.Database.GetConnectionString();
        }

        public async Task<FranchiseDashboardDto> GetFranchiseDashboardAsync(int franchiseId)
        {
            var dashboard = new FranchiseDashboardDto();

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                // Ek hi call mein saara data read karna (Multiple Result Sets)
                using (var multi = await db.QueryMultipleAsync("[dbo].[sp_GetFranchiseDashboard]",
                    new { FranchiseId = franchiseId },
                    commandType: CommandType.StoredProcedure))
                {
                    // 1. First Result Set: Main Summary
                    dashboard.Summary = (await multi.ReadAsync<SummaryDto>()).FirstOrDefault() ?? new SummaryDto();

                    // 2. Second Result Set: Main Franchise Stock (Store)
                    dashboard.MainStock = (await multi.ReadAsync<StockBreakdownDto>()).ToList();

                    // 3. Third Result Set: Member-wise Stock
                    dashboard.MemberStocks = (await multi.ReadAsync<MemberStockDto>()).ToList();
                }
            }

            return dashboard;
        }
    }
}