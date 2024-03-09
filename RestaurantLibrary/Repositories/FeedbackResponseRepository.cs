using Microsoft.EntityFrameworkCore;
using RestaurantRepositoryLibrary.DTO;
using RestaurantRepositoryLibrary.Repositories.Interfaces;

namespace RestaurantRepositoryLibrary.Repositories
{
    public class FeedbackResponseRepository : IFeedbackResponseRepository
    {
        private readonly RestaurantContext _restaurantContext;

        public FeedbackResponseRepository(RestaurantContext restaurantContext)
            => (_restaurantContext) = (restaurantContext);

        public async Task<FeedbackResponse> Add(string comments)
        {
            var response = await _restaurantContext.FeedbackResponses.AddAsync(new FeedbackResponse
            {
                Comments = comments,
                CreatedDate = DateTime.UtcNow,
            });

            await _restaurantContext.SaveChangesAsync();

            return response.Entity;
        }
    }
}
