using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class MembershipCategoryModel
    {
        [Key]
        public string? MembershipCategoryNumber { get; set; }
        public string? MembershipCategoryDescription { get; set; }
        public string? MembershipCategoryTotalLoans { get; set; }
    }
}
