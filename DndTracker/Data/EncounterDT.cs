using Microsoft.EntityFrameworkCore.Storage.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace DndTracker.Data
{
    public class EncounterDT
    {
        public int? Id { get; set; }
        public string? Location { get; set; }

        public string? Title { get; set; }

        public int? BackgroundImageId { get; set; }
        public ICollection<byte>? BackgroundImage { get; set; } = null;

        public ICollection<EncounterBlock> EncounterBlocks { get; set; } = [];

    }
}
