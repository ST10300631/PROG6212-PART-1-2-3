using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    using RaceDayApi.Data;
    using RaceDayApi.Models;

    namespace RaceDayApi.Controllers
    {
        [ApiController]
        [Route("api")]
        public class CategoriesController : ControllerBase
        {
            private readonly AppDbContext _context;

            public CategoriesController(AppDbContext context)
            {
                _context = context;
            }

            // GET: /api/events/{eventId}/categories
            [HttpGet("events/{eventId}/categories")]
            [AllowAnonymous]
            public IActionResult GetCategories(int eventId)
            {
                var categories = _context.Categories
                    .Where(c => c.EventId == eventId)
                    .ToList();

                return Ok(categories);
            }

            // POST: /api/events/{eventId}/categories
            [HttpPost("events/{eventId}/categories")]
            [Authorize(Roles = "Organiser")]
            public IActionResult AddCategory(
                int eventId,
                [FromBody] CreateCategoryDto model)
            {
                var category = new Category
                {
                    EventId = eventId,
                    Name = model.Name,
                    DistanceKm = (decimal?)model.DistanceKm,
                    EntryFee = model.EntryFee
                };

                _context.Categories.Add(category);
                _context.SaveChanges();

                return CreatedAtAction(
                    nameof(GetCategories),
                    new { eventId },
                    category);
            }

            // PUT: /api/categories/{id}
            [HttpPut("categories/{id}")]
            [Authorize(Roles = "Organiser")]
            public IActionResult UpdateCategory(
                int id,
                [FromBody] UpdateCategoryDto model)
            {
                var category = _context.Categories.Find(id);

                if (category == null)
                    return NotFound();

                category.Name = model.Name;
            category.DistanceKm = (decimal?)model.DistanceKm;
                category.EntryFee = model.EntryFee;

                _context.SaveChanges();

                return Ok(category);
            }

            // DELETE: /api/categories/{id}
            [HttpDelete("categories/{id}")]
            [Authorize(Roles = "Organiser")]
            public IActionResult DeleteCategory(int id)
            {
                var category = _context.Categories.Find(id);

                if (category == null)
                    return NotFound();

                _context.Categories.Remove(category);
                _context.SaveChanges();

                return Ok(new
                {
                    message = $"Category {id} removed successfully"
                });
            }
        }
    }