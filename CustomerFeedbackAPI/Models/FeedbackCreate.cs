using RestaurantLibrary.DTOs;

namespace CustomerFeedbackAPI.Models
{
    public class FeedbackCreate
    {
        public string Comments { get; set; }
        public byte Rating { get; set; }

        public CustomerFeedback ToDTO(int restaurantId)
        {
            return new CustomerFeedback
            {
                RestaurantId = restaurantId,
                Comments = Comments,
                Rating = Rating,
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}
