using FeedbackResponseAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using RestaurantLibrary.DTOs;
using RestaurantRepositoryLibrary;

namespace FeedbackResponseAPI.Services
{
    public class FeedbackResponseService : IFeedbackResponseService
    {
        private readonly RestaurantContext _restaurantContext;

        public FeedbackResponseService(RestaurantContext restaurantContext)
         => (_restaurantContext) = (restaurantContext);

        public async Task<int?> CreateResponse(int customerFeedbackId, string comments)
        {
            var feedback = await _restaurantContext.CustomerFeedbacks.FirstOrDefaultAsync(x => x.Id.Equals(customerFeedbackId));

            if (feedback == null) return null;

            feedback.FeedbackResponse = new FeedbackResponse
            {
                Comments = comments,
                CreatedDate = DateTime.UtcNow,
            };

            await _restaurantContext.SaveChangesAsync();

            return feedback.FeedbackResponse.Id;
        }
    }
}
