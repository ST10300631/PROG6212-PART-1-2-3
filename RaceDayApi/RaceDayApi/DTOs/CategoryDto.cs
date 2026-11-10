namespace RaceDayApi.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal EntryFee { get; set; }
    }
}