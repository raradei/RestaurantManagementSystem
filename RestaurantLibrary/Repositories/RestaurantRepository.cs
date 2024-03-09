using Microsoft.EntityFrameworkCore;
using RestaurantLibrary.DTO;
using RestaurantRepositoryLibrary.Repositories.Interfaces;

namespace RestaurantRepositoryLibrary.Repositories
{
    public class RestaurantRepository: IRestaurantRepository
    {
        private readonly RestaurantContext _restaurantContext;

        public RestaurantRepository(RestaurantContext restaurantContext)
            => (_restaurantContext) = (restaurantContext);

        public async Task<Restaurant> Add(Restaurant model)
        {
            var entry = await _restaurantContext.Restaurants.AddAsync(model);

            await _restaurantContext.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<Restaurant> Get(int id)
        {
            var entry = await _restaurantContext.Restaurants.Include(x => x.Feedbacks).FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (entry != null)
            {
                return entry;
            }

            return null;
        }
    }
}
