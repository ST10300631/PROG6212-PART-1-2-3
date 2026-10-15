using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RaceDayApi.Models.Controllers
{
    [ApiController]
    [Route("api")]
    public class CategoriesController : ControllerBase
    {
        // GET: /api/events/{eventId}/categories
        [HttpGet("Event/{EventId}/Categories")]
        [AllowAnonymous]
        public IActionResult GetCategories(int eventId)
        {
            return Ok(new[]
            {
                new
                {
                    Id = 1,
                    Name = "5km Race",
                    DistanceKm = 5,
                    EntryFee = 100
                }
            });
        }

        // POST: /api/Events/{EventId}/Categories
        [HttpPost("Events/{EventId}/Categories")]
        [Authorize(Roles = "Organiser")]
        public IActionResult AddCategory(
            int eventId,
            [FromBody] CreateCategoryDto model)
        {
            return StatusCode(201, model);
        }

        // PUT: /api/categories/{id}
        [HttpPut("Categories/{id}")]
        [Authorize(Roles = "Organiser")]
        public IActionResult UpdateCategory(
            int id,
            [FromBody] UpdateCategoryDto model)
        {
            return Ok(new
            {
                updatedCategory = model
            });
        }

        // DELETE: /api/categories/{id}
        [HttpDelete("Categories/{id}")]
        [Authorize(Roles = "Organiser")]
        public IActionResult DeleteCategory(int id)
        {
            return Ok(new
            {
                message = $"Category {id} removed successfully"
            });
        }
    }
}