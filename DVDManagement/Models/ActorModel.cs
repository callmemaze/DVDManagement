using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class ActorModel
    {
        [Key]
        public string? ActorId { get; set; }
        public string? ActorFirstName { get; set; }
        public string? ActorSurname { get; set; }

    }
}
