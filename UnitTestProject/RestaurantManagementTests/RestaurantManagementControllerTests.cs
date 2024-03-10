using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RestaurantManagementAPI.Controllers;
using RestaurantManagementAPI.Interfaces;
using RestaurantManagementAPI.Models;
using System.Net;

namespace UnitTestProject.RestaurantManagementTests
{

    [TestFixture]
    public class RestaurantManagementControllerTests
    {
        private Mock<ILogger<RestaurantManagementController>> _mockLogger;
        private Mock<IRestaurantService> _mockRestaurantService;
        private RestaurantManagementController _controller;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<RestaurantManagementController>>();
            _mockRestaurantService = new Mock<IRestaurantService>();
            _controller = new RestaurantManagementController(_mockLogger.Object, _mockRestaurantService.Object);
        }

        [Test]
        public async Task GetDetails_Existing_Restaurant_Id_Should_Return_Ok()
        {
            var existingRestaurantId = 1;
            var expectedRestaurantDetails = new RestaurantDetails { Name = "ExistingRestaurant" };

            _mockRestaurantService.Setup(service => service.GetDetails(existingRestaurantId))
                .ReturnsAsync(expectedRestaurantDetails);

            var result = await _controller.GetDetails(existingRestaurantId);

            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult?.Value, Is.EqualTo(expectedRestaurantDetails));
        }

        [Test]
        public async Task GetDetails_Nonexistent_Restaurant_Id_Should_Return_NotFound()
        {
            var nonExistentRestaurantId = 100;

            _mockRestaurantService.Setup(service => service.GetDetails(nonExistentRestaurantId))
                .ReturnsAsync((RestaurantDetails?)null);

            var result = await _controller.GetDetails(nonExistentRestaurantId);

            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public async Task GetDetails_Service_Exception_Should_Return_InternalServerError()
        {
            var restaurantId = 1;
            _mockRestaurantService.Setup(service => service.GetDetails(restaurantId))
                .ThrowsAsync(new Exception("An error occurred"));

            var result = await _controller.GetDetails(restaurantId);

            Assert.IsInstanceOf<StatusCodeResult>(result.Result);
            var objectResult = result.Result as StatusCodeResult;
            Assert.That(objectResult?.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task Create_Restaurant_Should_Return_Created()
        {
            var request = new RestaurantCreate { Name = "Test Restaurant", Location = "Test Location" };
            var restaurantId = 1;

            _mockRestaurantService.Setup(service => service.Create(request))
                .ReturnsAsync(restaurantId);

            var result = await _controller.Create(request);

            Assert.IsInstanceOf<ObjectResult>(result);
            var statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.Value, Is.EqualTo(restaurantId));
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
        }

        [Test]
        public async Task Create_Restaurant_Service_Exception_Should_Return_InternalServerError()
        {
            var request = new RestaurantCreate { Name = "Test Restaurant", Location = "Test Location" };
            _mockRestaurantService.Setup(service => service.Create(request))
                .ThrowsAsync(new Exception("An error occurred"));

            var result = await _controller.Create(request);

            Assert.IsInstanceOf<StatusCodeResult>(result);
            Assert.That((result as StatusCodeResult)?.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
        }
    }
}
