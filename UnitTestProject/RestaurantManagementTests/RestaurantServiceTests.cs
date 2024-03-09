using FeedbackResponseAPI.Interfaces;
using FeedbackResponseAPI.Services;
using Moq;
using RestaurantRepositoryLibrary.Repositories.Interfaces;
using RestaurantRepositoryLibrary.DTO;

namespace UnitTestProject.RestaurantManagementTests
{

    [TestFixture]
    public class RestaurantServiceTests
    {
        private Mock<IFeedbackResponseRepository> _mockFeedbackResponseRepository;
        private Mock<ICustomerFeedbackRepository> _mockCustomerFeedbackRepository;
        private IFeedbackResponseService _feedbackResponseService;

        [SetUp]
        public void Setup()
        {
            _mockFeedbackResponseRepository = new Mock<IFeedbackResponseRepository>();
            _mockCustomerFeedbackRepository = new Mock<ICustomerFeedbackRepository>();
            _feedbackResponseService = new FeedbackResponseService(
                _mockFeedbackResponseRepository.Object,
                _mockCustomerFeedbackRepository.Object
            );
        }

        [Test]
        public async Task CreateResponse_With_Valid_CustomerFeedbackId_Should_Create_Response_And_Return_ResponseId()
        {
            var customerFeedbackId = 1;
            var comments = "Test response comments";
            var feedback = new CustomerFeedback { Id = customerFeedbackId };
            var responseId = 1;

            _mockCustomerFeedbackRepository.Setup(repo => repo.Get(customerFeedbackId))
                .ReturnsAsync(feedback);

            _mockFeedbackResponseRepository.Setup(repo => repo.Add(comments))
                .ReturnsAsync(new FeedbackResponse { Id = responseId });

            var result = await _feedbackResponseService.CreateResponse(customerFeedbackId, comments);

            Assert.That(result, Is.EqualTo(responseId));
        }

        [Test]
        public async Task CreateResponse_With_Invalid_CustomerFeedbackId_Should_Return_Null()
        {
            var invalidCustomerFeedbackId = 999;

            _mockCustomerFeedbackRepository.Setup(repo => repo.Get(invalidCustomerFeedbackId))
                .ReturnsAsync((CustomerFeedback?)null);

            var result = await _feedbackResponseService.CreateResponse(invalidCustomerFeedbackId, "Test response comments");

            Assert.IsNull(result);
        }
    }
}
