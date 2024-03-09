using RestaurantRepositoryLibrary.DTO;

namespace RestaurantRepositoryLibrary.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        public Task<Restaurant> Add(Restaurant model);
        public Task<Restaurant> Get(int id);
    }
}
