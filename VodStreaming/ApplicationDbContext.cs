using Microsoft.EntityFrameworkCore;
using VodStreaming.Models;

namespace VodStreaming
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Video> videos { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}
