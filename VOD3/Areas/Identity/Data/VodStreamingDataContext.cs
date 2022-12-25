using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VodStreaming.Areas.Identity.Data;
using VodStreaming.Models;

namespace VodStreaming.Areas.Identity.Data;

public class VodStreamingDataContext : IdentityDbContext<VodStreamingUsers>
{
    public VodStreamingDataContext(DbContextOptions<VodStreamingDataContext> options)
        : base(options)
    {
    }
    public DbSet<Video> videos { get; set; }
    public DbSet<Category> categories { get; set; }
    public DbSet<VodStreamingUsers> AspNetUsers{ get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        
    }

    private class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<VodStreamingUsers>
    {
        public void Configure(EntityTypeBuilder<VodStreamingUsers> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(255);
            builder.Property(u => u.LastName).HasMaxLength(255);

        }
    }

    public DbSet<VodStreaming.Models.Users> Users { get; set; }
}
