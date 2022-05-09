using DVDManagement.Data;
using DVDManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DVDManagement.Controllers;

public class MemberController : Controller
{
    private readonly AuthDbContext dataBaseContext;

    public MemberController(AuthDbContext db)
    {
        dataBaseContext = db;
    }
    // GET
    //function 3
    public IActionResult MemberSearch(string SearchString)
    {
        var member = dataBaseContext!.MemberModel!.ToList();
        var loan = dataBaseContext!.LoanModel!.ToList();
        var dvd = dataBaseContext!.DVDCopyModel!.ToList();

        var result = from l in loan
            join m in member on l?.MemberNumber equals m?.MemberNumber into table1
            from t in table1.ToList()
            select new MemberSearchViewModel
            {
                member = t,
                loan = l,
            };
        DateTime currentDate = DateTime.Now.AddDays(30);
        var list = result.Where(x => x?.loan?.DateOut >= currentDate).ToList();
        var lst = list.Where(x => x?.member?.MemberLastName == SearchString).ToList();
        return View(lst);
    }
    
    public IActionResult MemberTotalLoans()
    {
        String c = "0";
        var member = dataBaseContext.MemberModel?.ToList();
        var loan = dataBaseContext.LoanModel?.ToList();
        var membercat = dataBaseContext.MembershipCategoryModel?.ToList();

        var dvd = (from m in member
            join l in loan on m.MemberNumber equals l.MemberNumber into table1
            from l in table1.Distinct().ToList().Where(l => l.MemberNumber == m.MemberNumber).Distinct().ToList()

            join mc in membercat on m.MembershipCategoryNumber equals mc.MembershipCategoryNumber into table2
            from mc in table2.Distinct().ToList().Where(mc => mc.MembershipCategoryNumber == m.MembershipCategoryNumber)
            group new { l, m, mc } by new { m.MemberFirstName, m.MembershipCategoryNumber, mc.MembershipCategoryTotalLoans }
            into grp
            select new
            {
                grp.Key.MemberFirstName,
                grp.Key.MembershipCategoryNumber,
                grp.Key.MembershipCategoryTotalLoans,
                TotalLoans = grp.Count(),
            }).OrderBy(x => x.MemberFirstName);
        ViewBag.totalloans = dvd;
        return View(dvd);
    }
    
    //function 12
    public IActionResult MemberListNotBorrowed()
        {
            var loan = dataBaseContext.LoanModel?.ToList();
            var maxDate = from l in loan
                          group l by l.MemberNumber
                          into g
                          select new
                          {
                              MaxDates = (from l in g select l.DateOut).Max(),
                          };
            ViewBag.dates = maxDate.ToList();
            var members = dataBaseContext.MemberModel?.ToList();
            var dvdCopy = dataBaseContext.DVDCopyModel?.ToList();
            var dvdTitle = dataBaseContext.DVDTitleModel?.ToList();
            List<int> difference = new List<int>();
            dynamic details = null;

            foreach (var dd in ViewBag.dates)
            {

                DateTime today = DateTime.Now;
                var dates = DateTime.Parse(dd.MaxDates.ToString());
                var diff = (today - dates).Days;
                var data = (from l in loan
                            join m in members on l.MemberNumber equals m.MemberNumber
                            join dc in dvdCopy on l.CopyNumber equals dc.CopyNumber
                            join dt in dvdTitle on dc.DVDNumber equals dt.DVDNumber
                            where (31 > diff)
                            group new { l, m, dc, dt } by new { m.MemberFirstName, m.MemberLastName, m.MemberAddress, ViewBag.dates }
                            into grp
                            select new
                            {
                                grp.Key.MemberFirstName,
                                grp.Key.MemberLastName,
                                grp.Key.MemberAddress,
                                MaxDates = (from l in grp select l.l.DateOut).Max(),
                                Difference = diff
                            }).OrderBy(x => x.MemberFirstName);
                ViewBag.details = data;
                return View();
            }
            return View();
        }
}