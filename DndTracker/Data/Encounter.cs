using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DndTracker.Data
{
    [Table("Encounters")]
    public class Encounter
    {
        [Key]
        public int Id { get; set; }

        [Column("owner")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; } = null!;

        [Column("location")]
        public string? Location { get; set; } = string.Empty;

        [Column("title")]
        public string? Title { get; set; } = string.Empty;

        [Column("background_id")]
        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        [Column("countdown")]
        public int? Countdown { get; set; }

        public ICollection<EncounterBlock> EncounterBlocks { get; set; } = [];
    }
}
