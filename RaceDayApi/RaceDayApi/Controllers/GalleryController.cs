using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayApi.Data;
using RaceDayApi.Models;
using System.IO;
using System.Threading.Tasks;

namespace RaceDayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly BlobServiceClient _blobService;

        public GalleryController(AppDbContext context ,
            BlobServiceClient blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        // POST: api/Gallery/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                var galleryItem = new Gallery
                {
                    ImageBlobName = System.Text.Encoding.UTF8.GetBytes(file.FileName),

                    ImageData = memoryStream.ToArray(),

                    FileName = file.FileName,

                    ContentType = file.ContentType
                };

                // Note: Use _context.Images because that is the name of your DbSet in AppDbContext
                _context.Images.Add(galleryItem);
                await _context.SaveChangesAsync();
            }

            return Ok("Image successfully saved to the database.");
        }

        // GET: api/Gallery/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image == null || image.ImageData == null)
                return NotFound("Image not found.");

            // This returns the file directly as a viewable image in the browser (e.g., image/jpeg)
            return File(image.ImageData, image.ContentType ?? "image/jpeg");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gallery>>> GetImages()
        {
            var images = await _context.Images.ToListAsync();
            return Ok(images);
        }
    }
}
