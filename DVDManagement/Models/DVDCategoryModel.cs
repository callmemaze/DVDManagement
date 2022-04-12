using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class DVDCategoryModel
    {
        [Key]
        public string? CategoryNumber { get; set; }
        public string? CategoryDescriptoin{ get; set; }
        public string? AgeRestriction { get; set; }

    }
}
