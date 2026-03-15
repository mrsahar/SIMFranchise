using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SIMFranchise.Data;
using SIMFranchise.Interfaces;
using SIMFranchise.Interfaces.Dashboard;
using SIMFranchise.Interfaces.Inventory;
using SIMFranchise.Interfaces.Purchase;
using SIMFranchise.Mappings;
using SIMFranchise.Middlewares.SIMFranchise.Middlewares;
using SIMFranchise.Services;
using SIMFranchise.Services.Expense;
using SIMFranchise.Services.HR;
using SIMFranchise.Services.Products;
using SIMFranchise.Services.Sales;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Program.cs mein is tarah likhein
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
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
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHumanResourceService, HumanResourceService>();
builder.Services.AddScoped<IMasterAdminService, MasterAdminService>();

//Sales
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

//Dashboard
builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
// Isko hamesha app.UseAuthorization() se pehle likhna hai
app.UseAuthentication();
app.UseAuthorization();  
app.MapControllers();

app.Run();
