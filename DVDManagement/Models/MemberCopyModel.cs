using DVDManagement.Models;

namespace DVDManagement.Models
{
    public class MemberCopyModel
    {
        public LoanModel? loan { get; set; }
        public DVDCopyModel? dvd { get; set; }
        public MemberModel? member { get; set; }
    }
}