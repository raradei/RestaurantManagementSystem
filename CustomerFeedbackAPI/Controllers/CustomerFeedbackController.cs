using CustomerFeedbackAPI.Interfaces;
using CustomerFeedbackAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerFeedbackAPI.Controllers
{
    [ApiController]
    [Route("api/feedbacks")]
    public class CustomerFeedbackController : ControllerBase
    {
        private readonly ILogger<CustomerFeedbackController> _logger;
        private readonly ICustomerFeedbackService _customerFeedbackService;

        public CustomerFeedbackController(
            ILogger<CustomerFeedbackController> logger,
            ICustomerFeedbackService customerFeedbackService
        ) => (_logger, _customerFeedbackService) = (logger, customerFeedbackService);


        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetFeedback([FromRoute] int restaurantId)
        {
            try
            {
                var feedback = await _customerFeedbackService.GetRestaurantFeedback(restaurantId);
                return Ok(feedback);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [HttpPost("{restaurantId}")]
        public async Task<IActionResult> SubmitFeedback([FromRoute] int restaurantId, [FromBody] FeedbackCreate request)
        {
            try
            {
                var createdEntityId = await _customerFeedbackService.Create(restaurantId, request);
                return StatusCode((int)HttpStatusCode.Created, createdEntityId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
