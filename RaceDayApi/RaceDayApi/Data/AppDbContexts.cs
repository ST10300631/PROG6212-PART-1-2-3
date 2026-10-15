using Microsoft.EntityFrameworkCore;
using RaceDayApi.Models;
namespace CourseRegistrationSystem.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Event> Event{ get; set; }
        public DbSet<Organiser>Organiser { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Route_detail> Route_Details{ get; set; }
        public DbSet<Weather_Cast> Weather_Casts { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}