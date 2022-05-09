using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DVDManagement.Models
{
    public class LoanModel
    {
        [Key]
        public string? LoanNumber { get; set; }
        public string? LoanTypeNumber { get; set; }
        public string? CopyNumber { get; set; }
        public string? MemberNumber { get; set; }
        public DateTime? DateOut { get; set; }
        public DateTime? DateDue { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}
