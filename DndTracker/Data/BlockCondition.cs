using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DndTracker.Data
{

    [Table("BlockConditions")]
    public class BlockCondition
    {
        [Key]
        public int Id { get; set; }

        [Column("block_id")]
        public int EncounterBlockId { get; set; }
        public EncounterBlock EncounterBlock { get; set; } = null!;

        [Column("condition_id")]
        public int ConditionId { get; set; }
        public Condition Condition { get; set; } = null!;

        [Column("stack")]
        public int? Stack { get; set; } = null;

    }

    [Table("Conditions")]
    public class Condition
    {
        [Key]
        public int Id { get; set; }

        [Column("display_name", TypeName = "nvarchar(100)")]
        public string? Name { get; set; }

        [Column("image_id")]
        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        public ICollection<BlockCondition> Conditions { get; set; } = [];

    }
}
