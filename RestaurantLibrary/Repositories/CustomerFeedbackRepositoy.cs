using Microsoft.EntityFrameworkCore;
using RestaurantRepositoryLibrary.DTO;
using RestaurantRepositoryLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantRepositoryLibrary.Repositories
{
    public class CustomerFeedbackRepositoy : ICustomerFeedbackRepository
    {
        private readonly RestaurantContext _restaurantContext;

        public CustomerFeedbackRepositoy(RestaurantContext restaurantContext)
            => (_restaurantContext) = (restaurantContext);

        public async Task<CustomerFeedback> Add(CustomerFeedback model)
        {
            var entry = await _restaurantContext.CustomerFeedbacks.AddAsync(model);

            await _restaurantContext.SaveChangesAsync();

            return entry.Entity;
        }

        public async Task<CustomerFeedback> Get(int id)
        {
            return await _restaurantContext.CustomerFeedbacks.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<CustomerFeedback>> GetListByRestaurantId(int restaurantId)
        {
            return await _restaurantContext.CustomerFeedbacks
                .Where(x => x.RestaurantId.Equals(restaurantId))
                .Include(x => x.FeedbackResponse)
                .ToListAsync();
        }

        public async Task<CustomerFeedback> LinkResponse(int id, FeedbackResponse feedbackResponse)
        {
            var customerFeedback = await _restaurantContext.CustomerFeedbacks.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (customerFeedback == null) return null;

            customerFeedback.FeedbackResponse = feedbackResponse;

            await _restaurantContext.SaveChangesAsync();

            return customerFeedback;
        }
    }
}
