using System.ComponentModel.DataAnnotations;
namespace RaceDayApi.Models
{
    public class Gallery
    {
        [Key]
        public int Id { get; set; }
        public required byte[] ImageBlobName { get; set; }

        // Add these three properties so the controller code works perfectly:
        public required byte[] ImageData { get; set; }
        public required string FileName { get; set; }
        public required string ContentType { get; set; }
    }
}
