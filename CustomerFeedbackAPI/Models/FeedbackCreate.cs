using RestaurantLibrary.DTOs;
using System.ComponentModel.DataAnnotations;

namespace CustomerFeedbackAPI.Models
{
    public class FeedbackCreate
    {
        public string Comments { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
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
