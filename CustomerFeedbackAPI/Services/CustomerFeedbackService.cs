using CustomerFeedbackAPI.Interfaces;
using CustomerFeedbackAPI.Models;
using RestaurantRepositoryLibrary.Repositories.Interfaces;

namespace CustomerFeedbackAPI.Services
{
    public class CustomerFeedbackService : ICustomerFeedbackService
    {
        private readonly ICustomerFeedbackRepository _customerFeedbackRepository;

        public CustomerFeedbackService(ICustomerFeedbackRepository customerFeedbackRepository)
            => (_customerFeedbackRepository) = (customerFeedbackRepository);

        public async Task<int> Create(int restaurantId, FeedbackCreate request)
        {
            var entry = await _customerFeedbackRepository.Add(request.ToDTO(restaurantId));
            return entry.Id;
        }

        public async Task<IEnumerable<FeedbackDetails>> GetRestaurantFeedback(int restaurantId)
        {
            var entries = await _customerFeedbackRepository.GetListByRestaurantId(restaurantId);
            return entries.Select(x => FeedbackDetails.FromDTO(x));
        }
    }
}
