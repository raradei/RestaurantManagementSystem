using Microsoft.EntityFrameworkCore;
using RestaurantLibrary.DTO;

namespace RestaurantRepositoryLibrary
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { 
            Database.EnsureCreated();
        }

        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }
        public virtual DbSet<FeedbackResponse> FeedbackResponses { get; set; }

    }
}
