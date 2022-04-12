using System.ComponentModel.DataAnnotations;

namespace DVDManagement.Models
{
    public class LoanTypeModel
    {
        [Key]
        public string? LoanTypeNumber { get; set; }
        public string? LoanType { get; set; }
        public string? LoanDuration { get; set; }
    }
}
