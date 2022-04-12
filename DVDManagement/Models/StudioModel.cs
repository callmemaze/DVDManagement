using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class StudioModel
    {
        [Key]
        public string? StudioNumber { get; set; }
        public string? StudioName { get; set; }
    }
}
