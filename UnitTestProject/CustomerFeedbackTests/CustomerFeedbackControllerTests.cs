using CustomerFeedbackAPI.Controllers;
using CustomerFeedbackAPI.Interfaces;
using CustomerFeedbackAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace UnitTestProject.CustomerFeedbackTests
{
    [TestFixture]
    public class CustomerFeedbackControllerTests
    {
        private Mock<ILogger<CustomerFeedbackController>> _mockLogger;
        private Mock<ICustomerFeedbackService> _mockCustomerFeedbackService;
        private CustomerFeedbackController _controller;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<CustomerFeedbackController>>();
            _mockCustomerFeedbackService = new Mock<ICustomerFeedbackService>();
            _controller = new CustomerFeedbackController(_mockLogger.Object, _mockCustomerFeedbackService.Object);
        }

        [Test]
        public async Task GetFeedback_Existing_RestaurantId_Should_Return_Ok()
        {
            var existingRestaurantId = 1;
            var expectedFeedbackList = new List<FeedbackDetails> { new FeedbackDetails { Id = 1 }, new FeedbackDetails { Id = 2 } };

            _mockCustomerFeedbackService.Setup(service => service.GetRestaurantFeedback(existingRestaurantId))
                .ReturnsAsync(expectedFeedbackList);

            var result = await _controller.GetFeedback(existingRestaurantId);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(expectedFeedbackList));
        }

        [Test]
        public async Task GetFeedback_Nonexistent_RestaurantId_Should_Return_EmptyList()
        {
            var nonExistentRestaurantId = 100;

            _mockCustomerFeedbackService.Setup(service => service.GetRestaurantFeedback(nonExistentRestaurantId))
                .ReturnsAsync(new List<FeedbackDetails> { });

            var result = await _controller.GetFeedback(nonExistentRestaurantId);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.That(okResult?.Value, Is.Empty);
        }

        [Test]
        public async Task GetFeedback_Service_Exception_Should_Return_InternalServerError()
        {
            var restaurantId = 1;
            _mockCustomerFeedbackService.Setup(service => service.GetRestaurantFeedback(restaurantId))
                .ThrowsAsync(new Exception("An error occurred"));

            var result = await _controller.GetFeedback(restaurantId);

            Assert.IsInstanceOf<StatusCodeResult>(result);
            var objectResult = (StatusCodeResult)result;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
        }

        [Test]
        public async Task SubmitFeedback_Should_Return_Created()
        {
            var restaurantId = 1;
            var request = new FeedbackCreate { Comments = "Test comment", Rating = 5 };
            var createdEntityId = 1;

            _mockCustomerFeedbackService.Setup(service => service.Create(restaurantId, request))
                .ReturnsAsync(createdEntityId);

            var result = await _controller.SubmitFeedback(restaurantId, request);

            Assert.IsInstanceOf<ObjectResult>(result);
            var statusCodeResult = (ObjectResult)result;
            Assert.That(statusCodeResult.Value, Is.EqualTo(createdEntityId));
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
        }

        [Test]
        public async Task SubmitFeedback_Service_Exception_Should_Return_InternalServerError()
        {
            var restaurantId = 1;
            var request = new FeedbackCreate { Comments = "Test comment", Rating = 5 };

            _mockCustomerFeedbackService.Setup(service => service.Create(restaurantId, request))
                .ThrowsAsync(new Exception("An error occurred"));

            var result = await _controller.SubmitFeedback(restaurantId, request);

            Assert.IsInstanceOf<StatusCodeResult>(result);
            var objectResult = (StatusCodeResult)result;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
        }
    }
}
