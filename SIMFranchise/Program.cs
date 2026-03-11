using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.Interfaces;
using SIMFranchise.Interfaces.Dashboard;
using SIMFranchise.Interfaces.Inventory;
using SIMFranchise.Mappings;
using SIMFranchise.Middlewares.SIMFranchise.Middlewares;
using SIMFranchise.Services;
using SIMFranchise.Services.Expense;
using SIMFranchise.Services.HR;
using SIMFranchise.Services.Products;
using SIMFranchise.Services.Sales;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Program.cs mein is tarah likhein
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SimfranchiseManagementDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IFranchiseService, FranchiseService>();
builder.Services.AddScoped<ILedgerService, LedgerService>();

// product 
builder.Services.AddScoped<ICardProductService, CardProductService>();
builder.Services.AddScoped<ISimProductService, SimProductService>();
builder.Services.AddScoped<ILoadOperatorService, LoadOperatorService>();

// purches
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

//user / team / team member / role
builder.Services.AddScoped<IHumanResourceService, HumanResourceService>();
builder.Services.AddScoped<IMasterAdminService, MasterAdminService>();

//Sales
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

//Dashboard
builder.Services.AddScoped<IDashboardService, DashboardService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
