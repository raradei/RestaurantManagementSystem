using Microsoft.AspNetCore.Mvc;
using RestaurantManagementAPI.Interfaces;
using RestaurantManagementAPI.Models;
using System.Net;

namespace RestaurantManagementAPI.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantManagementController : ControllerBase
    {
        private readonly ILogger<RestaurantManagementController> _logger;
        private readonly IRestaurantService _restaurantService;

        public RestaurantManagementController(
            ILogger<RestaurantManagementController> logger,
            IRestaurantService restaurantService
        ) => (_logger, _restaurantService) = (logger, restaurantService);

        [HttpGet]
        public async Task<ActionResult<RestaurantCreate>> GetDetails(int id)
        {
            try
            {
                var restaurant = await _restaurantService.GetDetails(id);
                return restaurant != null ? Ok(restaurant) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RestaurantCreate request)
        {
            try
            {
                var id = await _restaurantService.Create(request);
                return StatusCode((int)HttpStatusCode.Created, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
