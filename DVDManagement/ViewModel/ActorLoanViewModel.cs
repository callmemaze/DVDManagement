using DVDManagement.Models;
namespace DVDManagement.ViewModel;

public class ActorLoanViewModel
{
    public ActorModel? Actor { get; set; }
    public DVDTitleModel? Dvd { get; set; }
    public CastMemberModel? CastMember { get; set; }
    public LoanModel? Loan { get; set; }
    public DVDCopyModel? DvdCopy { get; set; }
    public int LoanCount { get; set; }
}