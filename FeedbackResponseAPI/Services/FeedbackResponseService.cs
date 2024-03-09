using FeedbackResponseAPI.Interfaces;
using RestaurantRepositoryLibrary.Repositories.Interfaces;

namespace FeedbackResponseAPI.Services
{
    public class FeedbackResponseService : IFeedbackResponseService
    {
        private readonly IFeedbackResponseRepository _feedbackResponseRepository;
        private readonly ICustomerFeedbackRepository _customerFeedbackRepository;

        public FeedbackResponseService(
            IFeedbackResponseRepository feedbackResponseRepository,
            ICustomerFeedbackRepository customerFeedbackRepository
        ) => (_feedbackResponseRepository, _customerFeedbackRepository) = (feedbackResponseRepository, customerFeedbackRepository);

        public async Task<int?> CreateResponse(int customerFeedbackId, string comments)
        {
            var feedback = await _customerFeedbackRepository.Get(customerFeedbackId);

            if (feedback == null) return null;

            var entry = await _feedbackResponseRepository.Add(comments);

            await _customerFeedbackRepository.LinkResponse(customerFeedbackId, entry);

            return entry.Id;
        }
    }
}
