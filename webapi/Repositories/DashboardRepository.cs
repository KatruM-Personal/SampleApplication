using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using webapi;

namespace SampleAPI.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        public DashboardRepository(IConfiguration config, IOptions<ConnectionStringsOptions> options)
        {
            _config = config;
            _connectionString = options.Value.MyDatabaseConnection;
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

        public List<Practice> GetDetails()
        {
            var connectionString = "Server=DESKTOP-86EN9CI;Database=Practice;User Id=sa;Password=123;TrustServerCertificate=True";
            using var connection = new SqlConnection(connectionString);
            var query = "SELECT * FROM Practice (NOLOCK)";
            using var command = new SqlCommand(query, connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            var results = new List<Practice>();
            while (reader.Read())
            {
                var myObject = new Practice
                {
                    Id = (int)reader["id"],
                    Name = (string)reader["name"]
                };
                results.Add(myObject);
            }
            return results;

        }
    }
}
