using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestaurantLibrary.DTOs;

namespace RestaurantRepositoryLibrary
{
    public class RestaurantContext : DbContext
    {

        protected readonly IConfigureOptions<string> Configuration;
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }
        public DbSet<FeedbackResponse> FeedbackResponses { get; set; }

    }
}
