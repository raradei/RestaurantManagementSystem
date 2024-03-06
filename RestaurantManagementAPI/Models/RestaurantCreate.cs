using RestaurantLibrary.DTOs;

namespace RestaurantManagementAPI.Models
{
    public class RestaurantCreate
    {
        public string Name { get; set; }
        public string Location { get; set; }

        public Restaurant ToDTO()
        {
            return new Restaurant
            {
                Name = Name,
                Location = Location,
                CreatedDate = DateTime.UtcNow,
            };
        }
    }
}
