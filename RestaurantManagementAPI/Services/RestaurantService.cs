using Microsoft.EntityFrameworkCore;
using RestaurantLibrary.DTOs;
using RestaurantManagementAPI.Interfaces;
using RestaurantManagementAPI.Models;
using RestaurantRepositoryLibrary;

namespace RestaurantManagementAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantContext _restaurantContext;

        public RestaurantService(RestaurantContext restaurantContext)
            => (_restaurantContext) = (restaurantContext);

        public async Task<int> Create(RestaurantCreate request)
        {
            var restaurant = await _restaurantContext.Restaurants.AddAsync(request.ToDTO());

            await _restaurantContext.SaveChangesAsync();

            return restaurant.Entity.Id;
        }

        public async Task<RestaurantDetails> GetDetails(int id)
        {
            var restaurant = await _restaurantContext.Restaurants.Include(x => x.Feedbacks).FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (restaurant != null)
            {
                return RestaurantDetails.FromDTO(restaurant);
            }

            return null;
        }
    }
}
