using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SampleAPI.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using webapi.Models;

namespace SampleAPI.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] Login model)
        {
            if (!string.IsNullOrEmpty(model.Email) && IsValidEmail(model.Email))
            {
                var token = await _dashboardRepository.GetEmail(model.Email);
                return Ok(new { Token = token });
            }
            return NotFound(new { message = "Please enter valid Email of your choice" });
        }

        private static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
