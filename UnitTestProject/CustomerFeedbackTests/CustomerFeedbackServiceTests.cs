using CustomerFeedbackAPI.Interfaces;
using CustomerFeedbackAPI.Models;
using CustomerFeedbackAPI.Services;
using Moq;
using RestaurantRepositoryLibrary.DTO;
using RestaurantRepositoryLibrary.Repositories.Interfaces;

namespace UnitTestProject.CustomerFeedbackTests
{
    [TestFixture]
    public class CustomerFeedbackServiceTests
    {
        private Mock<ICustomerFeedbackRepository> _mockCustomerFeedbackRepository;
        private ICustomerFeedbackService _customerFeedbackService;

        [SetUp]
        public void Setup()
        {
            _mockCustomerFeedbackRepository = new Mock<ICustomerFeedbackRepository>();
            _customerFeedbackService = new CustomerFeedbackService(_mockCustomerFeedbackRepository.Object);
        }

        [Test]
        public async Task Create_With_Valid_RestaurantId_And_Request_Should_Create_Feedback_And_Return_FeedbackId()
        {
            var restaurantId = 1;
            var request = new FeedbackCreate { Comments = "Test comments", Rating = 5 };
            var feedback = new CustomerFeedback { Id = 1 };

            _mockCustomerFeedbackRepository.Setup(repo => repo.Add(It.IsAny<CustomerFeedback>()))
                .ReturnsAsync(feedback);

            var result = await _customerFeedbackService.Create(restaurantId, request);

            Assert.That(result, Is.EqualTo(feedback.Id));
        }

        [Test]
        public async Task GetRestaurantFeedback_Should_Return_List_Of_FeedbackDetails()
        {
            var restaurantId = 1;
            var feedbackList = new List<CustomerFeedback>
            {
                new CustomerFeedback { Id = 1, Comments = "Feedback 1", Rating = 4 },
                new CustomerFeedback { Id = 2, Comments = "Feedback 2", Rating = 3 }
            };

            _mockCustomerFeedbackRepository.Setup(repo => repo.GetListByRestaurantId(restaurantId))
                .ReturnsAsync(feedbackList);

            var result = await _customerFeedbackService.GetRestaurantFeedback(restaurantId);

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(feedbackList.Count));

            foreach (var feedback in feedbackList)
            {
                Assert.IsTrue(result.Any(fd => fd.Id == feedback.Id && fd.Comments == feedback.Comments && fd.Rating == feedback.Rating));
            }
        }
    }
}
