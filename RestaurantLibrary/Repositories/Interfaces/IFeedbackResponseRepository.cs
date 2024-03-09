using RestaurantRepositoryLibrary.DTO;

namespace RestaurantRepositoryLibrary.Repositories.Interfaces
{
    public interface IFeedbackResponseRepository
    {
        public Task<FeedbackResponse> Add(string comments);
    }
}
