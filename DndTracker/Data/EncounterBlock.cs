using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DndTracker.Data
{
    public enum BlockTypes
    {
        Monster,
        Player,
        NPC
    }

    [Table("EncounterBlocks")]
    public class EncounterBlock
    {
        [Key]
        public int Id { get; set; }

        [Column("preset_id")]
        public int BlockPresetId { get; set; }
        public BlockPreset BlockPreset { get; set; } = null!;

        [Column("current_health")]
        public int? CurrentHealth { get; set; }

        [Column("temp_health")]
        public int? TempHealth { get; set; }

        [Column("initiative")]
        public int? Initiative { get; set; }

        [Column("visibility")]
        public bool Visibility { get; set; }

        [Column("encounter_id")]
        public int? EncounterId { get; set; }
        public Encounter? Encounter { get; set; }

        public ICollection<BlockCondition> Conditions { get; set; } = [];
    }

    [Table("EncounterBlockPresets")]
    public class BlockPreset
    {
        [Key]
        public int Id { get; set; }

        [Column("image_id")]
        public int ImageId { get; set; }
        public Image? Image { get; set; }

        [Column("display_name", TypeName = "nvarchar(100)")]
        public string? Name { get; set; }

        [Column("type")]
        public BlockTypes Type { get; set; }

        [Column("max_health")]
        public int? MaxHealth { get; set; }

    }
}
