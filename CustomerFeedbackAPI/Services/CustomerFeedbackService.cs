using CustomerFeedbackAPI.Interfaces;
using CustomerFeedbackAPI.Models;
using Microsoft.EntityFrameworkCore;
using RestaurantLibrary.DTOs;
using RestaurantRepositoryLibrary;

namespace CustomerFeedbackAPI.Services
{
    public class CustomerFeedbackService : ICustomerFeedbackService
    {
        private readonly RestaurantContext _restaurantContext;
        public CustomerFeedbackService(RestaurantContext restaurantContext)
            => (_restaurantContext) = (restaurantContext);
        public async Task<int> Create(int restaurantId, FeedbackCreate request)
        {
            var customerFeedback = await _restaurantContext.CustomerFeedbacks.AddAsync(request.ToDTO(restaurantId));

            await _restaurantContext.SaveChangesAsync();

            return customerFeedback.Entity.Id;
        }

        public async Task<IEnumerable<FeedbackDetails>> GetRestaurantFeedback(int restaurantId)
        {
            return await _restaurantContext.CustomerFeedbacks
                .Where(x => x.RestaurantId.Equals(restaurantId))
                .Include(x => x.FeedbackResponse)
                .Select(x => FeedbackDetails.FromDTO(x))
                .ToListAsync();
        }
    }
}
