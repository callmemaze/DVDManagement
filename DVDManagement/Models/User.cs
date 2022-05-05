using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class User
    {
        [Key]
        public int UserNumber { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? username { get; set; }
        [Required(ErrorMessage = "User type is required")]
        public string? type { get; set; }
        public string? password { get; set; }
    }
}