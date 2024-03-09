using RestaurantManagementAPI.Interfaces;
using RestaurantManagementAPI.Models;
using RestaurantRepositoryLibrary.Repositories.Interfaces;

namespace RestaurantManagementAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
            => (_restaurantRepository) = (restaurantRepository);

        public async Task<int> Create(RestaurantCreate request)
        {
            var restaurant = await _restaurantRepository.Add(request.ToDTO());

            return restaurant.Id;
        }

        public async Task<RestaurantDetails> GetDetails(int id)
        {
            var restaurant = await _restaurantRepository.Get(id);

            if (restaurant != null)
            {
                return RestaurantDetails.FromDTO(restaurant);
            }

            return null;
        }
    }
}
