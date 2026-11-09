namespace RaceDaySystem.Client.Models
{
    public class Gallery
    {
    
            public int Id { get; set; }
            public byte[] ImageBlobName { get; set; } = Array.Empty<byte>();
            public byte[] ImageData { get; set; } = Array.Empty<byte>();
            public string FileName { get; set; } = string.Empty;
            public string ContentType { get; set; } = string.Empty;
        
    }
}

