using Microsoft.AspNetCore.Mvc;
using Moq;
using SampleAPI.Controllers;
using SampleAPI.Repositories;
using webapi.Models;

namespace SampleTestProject
{
    public class UnitTest1
    {
        private readonly DashboardController _dashboardController;
        private readonly Mock<IDashboardRepository> _dashboardRepository;
        public UnitTest1()
        {
            _dashboardRepository = new Mock<IDashboardRepository>();            
            _dashboardController = new DashboardController(_dashboardRepository.Object);
        }
        [Fact]
        public async Task Login_NotFound()
        {
            //Arrange
            var loginDetails = new Login()
            {
                Email = "mkatru"
            };

            //Act
            var result = await _dashboardController.Login(loginDetails);
            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task Login_Valid()
        {
            //Arrange
            var loginDetails = new Login()
            {
                Email = "mkatru@gmail.com"
            };
            _dashboardRepository.Setup(repo => repo.GetEmail(loginDetails.Email));
            var dashboardController = new DashboardController(_dashboardRepository.Object);
            //Act
            var result = await dashboardController.Login(loginDetails);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}