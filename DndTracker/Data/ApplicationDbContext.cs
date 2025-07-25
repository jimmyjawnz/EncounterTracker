using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;

namespace DndTracker.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Encounter> Encounters { get; set; }
        public DbSet<EncounterBlock> Blocks { get; set; }
        public DbSet<BlockPreset> BlockPresets { get; set; }
        public DbSet<Image> Images { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var file = File.ReadAllBytes(@"wwwroot\images\no-profile.jpg");

            builder.Entity<Image>().HasData(
                new Image
                {
                    Id = 1,
                    Bytes = file,
                    Name = "no-profile.jpg"
                }
            );
        }
    }
}
