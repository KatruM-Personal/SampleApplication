using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SampleAPI.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IConfiguration _config;
        public DashboardRepository(IConfiguration config) 
        {
            _config = config;
        }
        public async Task<string> GetEmail(string email)
        {
            return await GenerateToken(email);
        }
        // To generate token
        private Task<string> GenerateToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,email),
                new Claim(ClaimTypes.Email, email)

            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));

        }
    }
}
