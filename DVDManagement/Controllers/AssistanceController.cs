using System.Globalization;
using DVDManagement.Data;
using DVDManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DVDManagement.Controllers;

[Authorize(Roles = "Admin,Assistance")]
public class AssistanceController : Controller
{
    private readonly AuthDbContext dataBaseContext;

    public AssistanceController(AuthDbContext db)
    {
        dataBaseContext = db;
    }
    // GET
    //function 4
    public IActionResult ProducerDvdList()
        {
            var dvdTitle = dataBaseContext.DVDTitleModel?.ToList();
            var producer = dataBaseContext.ProducerModel?.ToList();
            var castMember = dataBaseContext.CastMemberModel?.ToList();
            var studio = dataBaseContext.StudioModel?.ToList();
            var actor = dataBaseContext.ActorModel?.ToList();
            var listProducer = from dt in dvdTitle
                               join c in castMember on dt.DVDNumber equals c.DVDNumber into table1
                               from c in table1.ToList().Where(c => c.DVDNumber == dt.DVDNumber).ToList()

                               join p in producer on dt.ProducerNumber equals p.ProducerNumber into table2
                               from p in table2.ToList().Where(p => p.ProducerNumber == dt.ProducerNumber).ToList()

                               join s in studio on dt.StudioNumber equals s.StudioNumber into table3
                               from s in table3.ToList().Where(s => s.StudioNumber == dt.StudioNumber).ToList()

                               join a in actor on c.ActorNumber equals a.ActorId into table4
                               from a in table4.ToList().Where(a => a.ActorId == c.ActorNumber).ToList()
                               orderby dt.DVDReleased ascending, a.ActorSurname ascending
                               select new { dvdTitle = dt, castMember = c, actorDetails = a, studio = s, producer = p };
            
            ViewBag.listProducer = listProducer;

            return View(listProducer);
        }
    
    //function 9
    public IActionResult CreateDVDTitle(bool IsSuccess = false)
    {
        ViewBag.IsSuccess = IsSuccess;
        ViewBag.producer = dataBaseContext.ProducerModel;
        ViewBag.actor = dataBaseContext.ActorModel;
        ViewBag.category = dataBaseContext.DVDCategoryModel;
        ViewBag.studio = dataBaseContext.StudioModel;
        return View();
    }

    public async Task<IActionResult> AddDVDTitle
        (DVDTitleModel dvd, CastMemberModel cast, string dvdNumber,string producerNumber, string actorNumber, string categoryNumber, string studioNumber, string dvdTitleName, string? dvdReleased, string standardCharge, string penaltyCharge)
    {

        dvd.StudioNumber = studioNumber;
        dvd.ProducerNumber = producerNumber;
        dvd.CategoryNumber = categoryNumber;
        dvd.DVDNumber = "1";
        dvd.DVDTitle = dvdTitleName;
        dvd.DVDReleased = dvdReleased;
        dvd.StandardCharge = standardCharge;
        dvd.PenaltyCharge = penaltyCharge;


        if (ModelState.IsValid)
        {
            dataBaseContext.DVDTitleModel?.Update(dvd);
            var result = await dataBaseContext.SaveChangesAsync();

            await Task.Delay(TimeSpan.FromSeconds(3));
            //getting recent record of dvd number after some time delay
            var last = dataBaseContext.DVDTitleModel?.OrderBy(x => x.DVDNumber).LastOrDefault();
            Console.WriteLine(last?.DVDNumber);

            //after getting recent dvd number, creating new cast member record with that dvd number
            cast.ActorNumber = actorNumber;
            cast.DVDNumber = last?.DVDNumber;
            dataBaseContext.CastMemberModel?.Update(cast);
            await dataBaseContext.SaveChangesAsync();

            Console.WriteLine(result);
            return RedirectToAction("CreateDVDTitle", new { IsSuccess = true });

        }

        return View();
    }
    
    //function 6
    public IActionResult AddDVDCopy(string memberNumber)
        {
            var Memberage = dataBaseContext.MemberModel.Where(x => x.MemberNumber == memberNumber);
            ViewBag.MemberAge = Memberage;

            String dob = ViewBag.MemberAge.MemberDateOfBirth;//GETTING MEMBER DOB
            String todaysDate = DateTime.Now.ToShortDateString();

            //MEMBER NUMBER
            ViewBag.memberNumber = memberNumber;

            //CONVERTING IN DATE TIME
            DateTime cDOB = DateTime.Parse(dob);
            DateTime ctodaysDate = DateTime.Parse(todaysDate);

            //CALCULATING YEARS FOR AGE
            TimeSpan dayDiff = ctodaysDate.Subtract(cDOB);
            Console.Write(dayDiff.Days.ToString());
            var age = dayDiff.Days / 365;
            Console.Write(age);

            ViewBag.memAge = age;
            
            var loans = dataBaseContext.LoanModel.ToList();
            var members = dataBaseContext.MemberModel.ToList();
            var memberCategory = dataBaseContext.MembershipCategoryModel.ToList();

            var details = from l in loans
                          join m in members on l.MemberNumber equals m.MemberNumber into table1
                          from m in table1.Where(m => m.MemberNumber == l.MemberNumber && m.MemberNumber == memberNumber).ToList()

                          join mc in memberCategory on m.MembershipCategoryNumber equals mc.MembershipCategoryNumber into table2
                          from mc in table2.Where(mc => mc.MembershipCategoryNumber == m.MembershipCategoryNumber).ToList()
                          group new { l, m, mc } by new { m.MemberNumber, m.MembershipCategoryNumber, mc.MembershipCategoryTotalLoans }
                          into grp
                          select new
                          {
                              grp.Key.MemberNumber,
                              grp.Key.MembershipCategoryNumber,
                              grp.Key.MembershipCategoryTotalLoans,
                              TotalLoans = grp.Count()
                          };
            ViewBag.Details = details;
            //{ MembershipNumber = 8, MembershipCategoryNumber = 5, MembershipCategoryTotalLoans = "20", TotalLoans = 8 }
            Console.WriteLine("============================================");
            Console.WriteLine(details);
            //FOR LOANTYPE
            ViewBag.LoanTypeNumber = dataBaseContext.LoanTypeModel.ToList();

            //FOR COPY NUMBER
            ViewBag.Copy = dataBaseContext.DVDCopyModel?.ToList();
            return View();
        }
    
    //function 13
    
    public IActionResult GetDVDofNoLoan()
    {
        DateTime currentDate = DateTime.Now.Date;
        DateTime lastDate = currentDate.Subtract(new TimeSpan(31, 0, 0, 0, 0));
        String d = "0";
        var dvdTitle = dataBaseContext.DVDTitleModel?.ToList();
        var loan = dataBaseContext.LoanModel?.ToList();
        var dvdCopy = dataBaseContext.DVDCopyModel?.ToList();

        var dvd = from dt in dvdTitle
            join dc in dvdCopy on dt.DVDNumber equals dc.DVDNumber into table1
            from dc in table1
            join l in loan on dc.CopyNumber equals l.CopyNumber into table2
            from l in table2.Distinct().ToList().Where(l => l.CopyNumber == dc.CopyNumber && l.DateReturned.ToString() == d && (l.DateOut) >= lastDate).Distinct().ToList()

            select new { dvdTitle = dt, loan = l, dvdCopy = dc };
        
        ViewBag.dvd = dvd;
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