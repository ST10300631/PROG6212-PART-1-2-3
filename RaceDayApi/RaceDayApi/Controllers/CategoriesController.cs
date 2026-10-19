using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayApi.Models;
using RaceDayApi.Data;

namespace RaceDayApi.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("api/events/{eventId}/categories")]
        public async Task<IActionResult> GetCategories(int eventId)
        {
            var categories = await _context.Categories
                .Where(c => c.EventId == eventId)
                .ToListAsync();

            return Ok(categories);
        }

        [Authorize(Roles = "Organiser")]
        [HttpPost("api/events/{eventId}/categories")]
        public async Task<IActionResult> AddCategory(
            int eventId,
            Category category)
        {
            category.EventId = eventId;

            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return StatusCode(201, category);
        }

        [Authorize(Roles = "Organiser")]
        [HttpPut("api/categories/{id}")]
        public async Task<IActionResult> UpdateCategory(
            int id,
            Category category)
        {
            var existing = await _context.Categories.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.Name = category.Name;
            existing.Description = category.Description;
            existing.EntryFee = category.EntryFee;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        [Authorize(Roles = "Organiser")]
        [HttpDelete("api/categories/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Category deleted"
            });
        }
    }
}