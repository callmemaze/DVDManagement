using DVDManagement.Models;

namespace DVDManagement.ViewModel
{
    public class LoanListViewModel
    {
        public DVDCopyModel? dvdCopy { get; set; }
        public DVDTitleModel? dvd { get; set; }
        public LoanModel? loan { get; set; }
        public MemberModel? member { get; set; }
    }
}