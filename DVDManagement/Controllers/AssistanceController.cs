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
    (DVDTitleModel dvd, CastMemberModel cast, string dvdNumber, string producerNumber, string actorNumber,
        string categoryNumber, string studioNumber, string dvdTitleName, string? dvdReleased, string standardCharge,
        string penaltyCharge)
    {
        Random r = new Random();
        dvd.StudioNumber = studioNumber;
        dvd.ProducerNumber = producerNumber;
        dvd.CategoryNumber = categoryNumber;
        dvd.DVDNumber = r.Next().ToString();
        dvd.DVDTitle = dvdTitleName;
        dvd.DVDReleased = dvdReleased;
        dvd.StandardCharge = standardCharge;
        dvd.PenaltyCharge = penaltyCharge;



        dataBaseContext.DVDTitleModel?.Add(dvd);
        var result = await dataBaseContext.SaveChangesAsync();

        await Task.Delay(TimeSpan.FromSeconds(3));
        //getting recent record of dvd number after some time delay
        var last = dataBaseContext.DVDTitleModel?.OrderBy(x => x.DVDNumber).LastOrDefault();
        Console.WriteLine(last?.DVDNumber);

        //after getting recent dvd number, creating new cast member record with that dvd number
        cast.ActorNumber = actorNumber;
        cast.DVDNumber = last?.DVDNumber;
        cast.CastMemberModelNo = r.Next().ToString();
        dataBaseContext.CastMemberModel?.Add(cast);
        await dataBaseContext.SaveChangesAsync();

        Console.WriteLine(result);
        return RedirectToAction("CreateDVDTitle", new { IsSuccess = true });


    }

    //function 6
    public IActionResult AddDVDCopy() {
        var dvdcopy = dataBaseContext.DVDCopyModel?.ToList();
        var dvdtitle = dataBaseContext.DVDTitleModel?.ToList();

        var members = dataBaseContext.MemberModel?.ToList();
            
        var loanType = dataBaseContext.LoanTypeModel?.ToList();

        ViewBag.member = members;
        ViewBag.loanType = loanType;

        var dvd = from dc in dvdcopy
            join dt in dvdtitle on dc.DVDNumber equals dt.DVDNumber
            select new { 
                dvdtitle = dt,
                dvdcopy = dc,
            };
        ViewBag.dvd = dvd;

        return View();
    }
    
    
        [HttpPost]
        public async Task<IActionResult> AddDVDCopy(LoanModel Loan, string member, string loantype, string copynumber)
        {
            var memberInfo = dataBaseContext.MemberModel.Where(x => x.MemberNumber == member).First();
            DateTime dob = memberInfo.MemberDateOfBirth;//GETTING MEMBER DOB
            String todaysDate = DateTime.Now.ToShortDateString();

            var today = DateTime.Now.ToShortDateString();


            //CONVERTING IN DATE TIME
            DateTime cDOB = dob;
            DateTime ctodaysDate = DateTime.Parse(todaysDate);

            TimeSpan dayDiff = ctodaysDate.Subtract(cDOB);
            Console.Write(dayDiff.Days.ToString());
            var age = dayDiff.Days / 365;
            Console.Write(age);

            

            var dvd = dataBaseContext.DVDTitleModel?.ToList();
            var catogory = dataBaseContext.DVDCategoryModel?.ToList();
            // var dvdCopy = _dbcontext.DVDCopys.ToList();
            var dvdCopy = dataBaseContext.DVDCopyModel?.Where(x => x.CopyNumber == copynumber).First();
            var dvdInfo = dataBaseContext.DVDTitleModel?.Where(x => x.DVDNumber == dvdCopy.DVDNumber).First();

            var agerestriction = dvdInfo?.DVDCategoryModel?.AgeRestriction;

            Random r = new Random();

            Loan.LoanNumber = r.Next().ToString();
            Loan.MemberNumber = member;
            Loan.LoanTypeNumber = loantype;
            Loan.CopyNumber = copynumber;
            Loan.DateOut = DateTime.Now;
            
            Loan.DateDue = DateTime.Now.AddMonths(3);
            if (agerestriction == "false")
            {
                dataBaseContext.LoanModel?.Add(Loan);
                await dataBaseContext.SaveChangesAsync();

                return RedirectToAction("AddDVDCopy","Assistance");
            }
            if (agerestriction == "true")
            {
                if (age > 18)
                {
                    dataBaseContext.LoanModel?.Add(Loan);
                    await dataBaseContext.SaveChangesAsync();
                    return RedirectToAction("AddDVDCopy","Assistance");
                }

                ViewBag.message = "hello";
                return RedirectToAction("AddDVDCopy","Assistance");

                //cannot loan the dvd due to age restriction
            }
            return RedirectToAction("AddDVDCopy","Assistance");

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
    
    

    
    
    
}