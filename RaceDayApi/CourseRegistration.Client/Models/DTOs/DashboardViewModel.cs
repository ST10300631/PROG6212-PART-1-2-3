using System.Collections.Generic;

namespace RaceDaySystem.Client.Models.DTOs
{
    public class DashboardViewModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

        // FIX: Changed from EventDto to CreateEventDto to clear type conversion errors
        public IEnumerable<CreateEventDto> Events { get; set; } = new List<CreateEventDto>();

        public IEnumerable<ResultDto> Results { get; set; } = new List<ResultDto>();

        // FIX: Using default! skips the required property checks for class initialization limits
        public CreateEventDto NewEvent { get; set; } = default!;
    }
}

