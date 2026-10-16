using Microsoft.EntityFrameworkCore;
using RaceDayApi.Models;

namespace RaceDayApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Organiser> Organisers { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Result> Results { get; set; }
        public IEnumerable<object> Category { get; internal set; }
    }
}