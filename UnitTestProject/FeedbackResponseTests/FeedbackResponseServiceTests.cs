using Moq;
using RestaurantRepositoryLibrary.DTO;
using RestaurantManagementAPI.Interfaces;
using RestaurantManagementAPI.Models;
using RestaurantManagementAPI.Services;
using RestaurantRepositoryLibrary.Repositories.Interfaces;

namespace UnitTestProject.FeedbackResponseTests
{
    [TestFixture]
    public class FeedbackResponseServiceTests
    {
        private Mock<IRestaurantRepository> _mockRestaurantRepository;
        private IRestaurantService _restaurantService;

        [SetUp]
        public void Setup()
        {
            _mockRestaurantRepository = new Mock<IRestaurantRepository>();
            _restaurantService = new RestaurantService(_mockRestaurantRepository.Object);
        }

        [Test]
        public async Task Create_Should_Add_New_Restaurant_And_Return_Id()
        {
            var request = new RestaurantCreate { Name = "Test Restaurant", Location = "Test Location" };
            var restaurantDto = new Restaurant { Id = 1, Name = request.Name, Location = request.Location };

            _mockRestaurantRepository
                .Setup(repo => repo.Add(It.IsAny<Restaurant>()))
                .ReturnsAsync(restaurantDto);

            var result = await _restaurantService.Create(request);

            Assert.That(result, Is.EqualTo(restaurantDto.Id));
        }

        [Test]
        public async Task GetDetails_With_Valid_Id_Should_Return_RestaurantDetails()
        {
            var restaurantId = 1;
            var restaurantDto = new Restaurant { Id = restaurantId, Name = "Test Restaurant", Location = "Test Location" };
            var expectedDetails = new RestaurantDetails { Name = restaurantDto.Name, Location = restaurantDto.Location };

            _mockRestaurantRepository.Setup(repo => repo.Get(restaurantId))
                .ReturnsAsync(restaurantDto);

            var result = await _restaurantService.GetDetails(restaurantId);

            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(expectedDetails.Name));
            Assert.That(result.Location, Is.EqualTo(expectedDetails.Location));
        }

        [Test]
        public async Task GetDetails_With_Invalid_Id_Should_Return_Null()
        {
            var invalidRestaurantId = 999;

            _mockRestaurantRepository.Setup(repo => repo.Get(invalidRestaurantId))
                .ReturnsAsync((Restaurant?)null);

            var result = await _restaurantService.GetDetails(invalidRestaurantId);

            Assert.IsNull(result);
        }
    }
}
