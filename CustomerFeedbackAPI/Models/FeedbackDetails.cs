using RestaurantLibrary.DTOs;

namespace CustomerFeedbackAPI.Models
{
    public class FeedbackDetails
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public byte Rating { get; set; }

        public string Response { get; set; }
        public DateTime CreatedDate { get; set; }

        public static FeedbackDetails FromDTO(CustomerFeedback customerFeedback)
        {
            return new FeedbackDetails
            {
                Id = customerFeedback.Id,
                Comments = customerFeedback.Comments,
                Rating = customerFeedback.Rating,
                Response = customerFeedback.FeedbackResponse?.Comments ?? null,
                CreatedDate = customerFeedback.CreatedDate
            };
        }
    }
}
