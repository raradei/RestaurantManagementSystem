using RestaurantManagementAPI.Models;

namespace RestaurantManagementAPI.Interfaces
{
    public interface IRestaurantService
    {
        public Task<int> Create(RestaurantCreate request);
        public Task<RestaurantDetails> GetDetails(int id);
    }
}
