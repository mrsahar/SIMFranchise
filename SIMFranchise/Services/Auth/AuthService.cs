using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Auth;
using SIMFranchise.Interfaces;

namespace SIMFranchise.Services
{
    public class AuthService : IAuthService
    {
        private readonly SimfranchiseManagementDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(SimfranchiseManagementDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            // 1. User ko DB mein verify karna
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.PasswordHash == dto.Password && u.IsActive == true);

            if (user == null) return null; // Agar user na mile to null wapas bhejein

            // 2. Token ke andar kya information hogi (Claims)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()),
            };

            // Agar Franchise User hai to FranchiseId bhi token mein daal dein
            if (user.FranchiseId.HasValue)
            {
                claims.Add(new Claim("FranchiseId", user.FranchiseId.Value.ToString()));
            }

            // 3. Secret Key tayyar karna
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. Token Generate karna
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(12), // 1 din baad token expire ho jayega
                signingCredentials: creds
            );

            // 5. Token ko string mein convert karna
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // 6. Response DTO wapas bhejna
            return new AuthResponseDto
            {
                Token = jwtToken,
                UserId = user.Id,
                Name = user.Name,
                RoleId = user.RoleId,
                FranchiseId = user.FranchiseId
            };
        }
    }
}