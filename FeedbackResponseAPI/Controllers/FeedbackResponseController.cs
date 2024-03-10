using FeedbackResponseAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FeedbackResponseAPI.Controllers
{
    [ApiController]
    [Route("api/feedback-responses")]
    public class FeedbackResponseController : ControllerBase
    {
        private readonly ILogger<FeedbackResponseController> _logger;
        private readonly IFeedbackResponseService _feedbackResponseService;

        public FeedbackResponseController(
            ILogger<FeedbackResponseController> logger,
            IFeedbackResponseService feedbackResponseService
        ) => (_logger, _feedbackResponseService) = (logger, feedbackResponseService);

        [HttpPost("{customerFeedbackId}")]
        public async Task<IActionResult> AddFeedbackResponse([FromRoute] int customerFeedbackId, [FromBody] string comments)
        {
            try
            {
                var createdEntityId = await _feedbackResponseService.CreateResponse(customerFeedbackId, comments);
                if (createdEntityId == null) return NotFound();
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
