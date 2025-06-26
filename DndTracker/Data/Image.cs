using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DndTracker.Data
{
    [Table("Images")]
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Column("image", TypeName = "image")]
        public byte[] Bytes { get; set; } = [];

        [Column("name", TypeName = "nvarchar(100)")]
        public string? Name { get; set; }
    }
}
