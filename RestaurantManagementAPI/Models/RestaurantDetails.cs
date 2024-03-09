using RestaurantLibrary.DTO;

namespace RestaurantManagementAPI.Models
{
    public class RestaurantDetails
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public double Rating { get; set; }

        public static RestaurantDetails FromDTO(Restaurant restaurant)
        {
            return new RestaurantDetails
            {
                Name = restaurant.Name,
                Location = restaurant.Location,
                Rating = restaurant.Feedbacks.Any() ? Convert.ToDouble(restaurant.Feedbacks.Average(x => x.Rating).ToString("0.00")) : 0,
            };
        }
    }
}
