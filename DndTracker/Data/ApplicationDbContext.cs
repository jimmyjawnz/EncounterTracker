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
        public DbSet<BlockCondition> BlockConditions { get; set; }
        public DbSet<Condition> Conditions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var profile_file = File.ReadAllBytes(@"wwwroot\images\no-profile.jpg");
            var hidden_file = File.ReadAllBytes(@"wwwroot\images\conditions\Invisible.png");
            var bleeding_file = File.ReadAllBytes(@"wwwroot\images\conditions\BleedingOut.png");
            var petrified_file = File.ReadAllBytes(@"wwwroot\images\conditions\Petrified.png");
            var restrained_file = File.ReadAllBytes(@"wwwroot\images\conditions\Restrained.png");

            builder.Entity<Image>().HasData(
                new Image
                {
                    Id = 1,
                    Bytes = profile_file,
                    Name = "no-profile.jpg"
                },
                new Image
                {
                    Id = 2,
                    Bytes = hidden_file,
                    Name = "Invisible.png"
                },
                new Image
                {
                    Id = 3,
                    Bytes = bleeding_file,
                    Name = "BleedingOut.png"
                },
                new Image
                {
                    Id = 4,
                    Bytes = petrified_file,
                    Name = "Petrified.png"
                },
                new Image
                {
                    Id = 5,
                    Bytes = restrained_file,
                    Name = "Restrained.png"
                }
            );

            builder.Entity<Condition>().HasData(
                new Condition
                {
                    Id = 1,
                    ImageId = 2,
                    Name = "Hidden"
                },
                new Condition
                {
                    Id = 2,
                    ImageId = 5,
                    Name = "Restrained"
                },
                new Condition
                {
                    Id = 3,
                    ImageId = 4,
                    Name = "Vunerable"
                }
            );
        }
    }
}
