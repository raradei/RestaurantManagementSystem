using FeedbackResponseAPI.Controllers;
using FeedbackResponseAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace UnitTestProject.CustomerFeedbackTests
{
    [TestFixture]
    public class FeedbackResponseControllerTests
    {
        private Mock<ILogger<FeedbackResponseController>> _mockLogger;
        private Mock<IFeedbackResponseService> _mockFeedbackResponseService;
        private FeedbackResponseController _controller;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<FeedbackResponseController>>();
            _mockFeedbackResponseService = new Mock<IFeedbackResponseService>();
            _controller = new FeedbackResponseController(_mockLogger.Object, _mockFeedbackResponseService.Object);
        }

        [Test]
        public async Task AddFeedbackResponse_ValidInput_Should_Return_Created()
        {
            var customerFeedbackId = 1;
            var comments = "Test response comments";
            var createdEntityId = 123;

            _mockFeedbackResponseService.Setup(service => service.CreateResponse(customerFeedbackId, comments))
                .ReturnsAsync(createdEntityId);

            var result = await _controller.AddFeedbackResponse(customerFeedbackId, comments);

            Assert.IsInstanceOf<ObjectResult>(result);
            var createdResult = (ObjectResult)result;
            Assert.That(createdResult.StatusCode, Is.EqualTo((int)HttpStatusCode.Created));
            Assert.That(createdResult.Value, Is.EqualTo(createdEntityId));
        }

        [Test]
        public async Task AddFeedbackResponse_Nonexistent_CustomerFeedbackId_Should_Return_NotFound()
        {
            var nonExistentCustomerFeedbackId = 100;
            var comments = "Test response comments";

            _mockFeedbackResponseService.Setup(service => service.CreateResponse(nonExistentCustomerFeedbackId, comments))
                .ReturnsAsync((int?)null);

            var result = await _controller.AddFeedbackResponse(nonExistentCustomerFeedbackId, comments);

            Assert.IsInstanceOf<NotFoundResult>(result);
            var objectResult = (NotFoundResult)result;
            Assert.That(objectResult.StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
        }
    }
}
