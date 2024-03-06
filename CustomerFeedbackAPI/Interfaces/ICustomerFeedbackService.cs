using CustomerFeedbackAPI.Models;

namespace CustomerFeedbackAPI.Interfaces
{
    public interface ICustomerFeedbackService
    {
        public Task<int> Create(int restaurantId, FeedbackCreate request);
        public Task<IEnumerable<FeedbackDetails>> GetRestaurantFeedback(int restaurantId);
    }
}
