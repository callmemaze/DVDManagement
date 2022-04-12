using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDManagement.Models
{
    public class MemberModel
    {
        [Key]
        public string? MemberNumber { get; set; }
        public string? MembershipCategoryNumber { get; set; }
        public string? MemberLastName { get; set; }
        public string? MemberFirstName { get; set; }
        public string? MemberAddress { get; set; }
        public DateTime MemberDateOfBirth { get; set; }

        [ForeignKey("MembershipCategoryNumber")]
        public MembershipCategoryModel? MembershipCategoryModel { get; set; }



    }
}
