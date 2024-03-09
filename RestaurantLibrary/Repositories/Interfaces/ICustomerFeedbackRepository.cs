using RestaurantRepositoryLibrary.DTO;

namespace RestaurantRepositoryLibrary.Repositories.Interfaces
{
    public interface ICustomerFeedbackRepository
    {
        public Task<CustomerFeedback> Add(CustomerFeedback model);
        public Task<CustomerFeedback> Get(int id);
        public Task<IEnumerable<CustomerFeedback>> GetListByRestaurantId(int restaurantId);
        public Task<CustomerFeedback> LinkResponse(int id, FeedbackResponse feedbackResponse);
    }
}
