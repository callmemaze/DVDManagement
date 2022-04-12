using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class ProducerModel
    {
        [Key]
        public string? ProducerNumber { get; set; }
        public string? ProducerName { get; set; }
    }
}
