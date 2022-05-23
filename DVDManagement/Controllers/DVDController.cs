using DVDManagement.Data;
using DVDManagement.Models;
using DVDManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DVDManagement.Controllers
{
   
    public class DVDController : Controller
    {
        private readonly AuthDbContext dataBaseContext;

        public DVDController(AuthDbContext db)
        {
            dataBaseContext = db;
        }

        //function 1
        public IActionResult SearchActor(string searchString)
        {
            var actor = dataBaseContext!.ActorModel!.ToList();
            var castMember = dataBaseContext!.CastMemberModel!.ToList();
            var dvd = dataBaseContext!.DVDTitleModel!.ToList();

            var result = from c in castMember
                         join d in dvd on c?.DVDNumber equals d?.DVDNumber into table1
                         from t in table1.ToList()
                         join a in actor on c.ActorNumber equals a.ActorId into table2
                         from i in table2.ToList()
            select new ActorViewModel
                         {
                             castMember = c,
                             dvd = t,
                             actor = i,
                         };
            if (!String.IsNullOrEmpty(searchString))
            {
                var list = result.Where(x => x?.actor?.ActorSurname == (searchString)).ToList();
                return View(list);
            }
            else
            {
                return View(result);
            }
            
        }
        //function 11
        public IActionResult SearchCastMember(string searchString)
        {
            var actor = dataBaseContext!.ActorModel!.ToList();
            var castMember = dataBaseContext!.CastMemberModel!.ToList();
            var dvd = dataBaseContext!.DVDTitleModel!.ToList();
            var dvdCopy = dataBaseContext!.DVDCopyModel!.ToList();
            var loan = dataBaseContext!.LoanModel!.ToList();

            var result = from c in castMember
                join d in dvd on c?.DVDNumber equals d?.DVDNumber into table1
                from t in table1.ToList()
                join a in actor on c.ActorNumber equals a.ActorId into table2
                from i in table2.ToList()
                join d2 in dvdCopy on t.DVDNumber equals d2.DVDNumber into table3
                from dt in table3.ToList()
                join l in loan on dt.CopyNumber equals l.CopyNumber into table4
                from lt in table4.ToList()
                select new ActorLoanViewModel
                {
                    CastMember = c,
                    Dvd = t,
                    Actor = i,
                    DvdCopy = dt,
                    Loan = lt,
                    LoanCount = table4.Count(x => x?.DateReturned == DateTime.Parse("0001-01-01 00:00:00.0000000"))
                };
            
            if (!String.IsNullOrEmpty(searchString))
            {
                var list = result.Where(x => x?.Actor?.ActorSurname == (searchString)).ToList();
                return View(list);
            }
            else
            {
                return View(result);
            }
            
        }
        //function 5
        public IActionResult CopyNumberSearch(string searchString)
        {
            var member = dataBaseContext!.MemberModel!.ToList();
            var loan = dataBaseContext!.LoanModel!.ToList();
            var dvd = dataBaseContext!.DVDCopyModel!.ToList();

            var result = from l in loan
                                 join m in member on l.MemberNumber equals m?.MemberNumber into table1
                                 from t in table1.ToList()
                                 join d in dvd on l.CopyNumber equals d.CopyNumber into table2
                                 from i in table2.ToList()
                                 select new MemberCopyModel
                                 {
                                     loan = l,
                                     dvd = i,
                                     member = t,
                                 };
            
            if (!String.IsNullOrEmpty(searchString))
            {
                var list = result.Where(x => x?.loan?.CopyNumber == searchString).ToList();
                return View(list);
            }
            else
            {
                return View(result);
            }
        }

        public IActionResult ProducerList()
        {
            var producer = dataBaseContext!.ProducerModel!.ToList();
            var studio = dataBaseContext!.StudioModel!.ToList();
            var actor = dataBaseContext!.ActorModel!.ToList();
            var castMember = dataBaseContext!.CastMemberModel!.ToList();
            var dvd = dataBaseContext!.DVDTitleModel!.ToList();

            var result = from c in castMember
                         join d in dvd on c?.DVDNumber equals d?.DVDNumber into table1
                         from t in table1.ToList()
                         join a in actor on c.ActorNumber equals a.ActorId into table2
                         from i in table2.ToList()
                         join s in studio on t.StudioNumber equals s.StudioNumber into table3
                         from st in table3.ToList()
                         join p in producer on t.ProducerNumber equals p.ProducerNumber into table4
                         from pt in table4.ToList()
                         select new ProducerViewModel
                         {
                             producer = pt,
                             dvd = t,
                             castMember = c,
                             studio = st,
                             actor = i,
                         };
            //var list = result.Where(x => x?.loan?.CopyNumber == SearchString).ToList();
            return View(result);
        }
        
        
        //function 10
        public IActionResult OldDvdCopyList(bool copyDeleted = false)
        {
            ViewBag.copyDeleted = copyDeleted;
            DateTime currentDate = DateTime.Now.Date;
            DateTime lastDate = currentDate.Subtract(new TimeSpan(365, 0, 0, 0, 0));
            String d = "0";
            var loan = dataBaseContext.LoanModel?.ToList();
            var dvdCopy = dataBaseContext.DVDCopyModel?.ToList();

            var dvd = from dc in dvdCopy
                join l in loan on dc.CopyNumber equals l.CopyNumber into table1
                from l in table1.Distinct().Where(l => l.CopyNumber == dc.CopyNumber && l.DateReturned != DateTime.Parse("0001-01-01 00:00:00.0000000") && dc.DatePurchased < lastDate)

                select new { loan = l, dvdCopy = dc };
            ViewBag.dvdList = dvd;
            return View(dvd);
        }
        
        public async Task<IActionResult> DeleteCopy(string copynumber)
        {
            var copy = dataBaseContext.DVDCopyModel.Where(l => l.CopyNumber == copynumber).First();
            dataBaseContext.DVDCopyModel.Remove(copy);
            dataBaseContext.SaveChanges();
            return RedirectToAction("OldDvdCopyList", new { copyDeleted = true });
        }
        //function 11
        public IActionResult NotLoanedDvdCopyList()
        {
            String c = "0";
            var member = dataBaseContext.MemberModel?.ToList();
            var loan = dataBaseContext.LoanModel?.ToList();
            var dvdTitle = dataBaseContext.DVDTitleModel?.ToList();
            var dvdCopy = dataBaseContext.DVDCopyModel?.ToList();

            var copyloan = (from l in loan
                            join m in member on l.MemberNumber equals m.MemberNumber into table1
                            from m in table1.Distinct().ToList().Where(m => m.MemberNumber == l.MemberNumber).Distinct().ToList()
                            join dc in dvdCopy on l.CopyNumber equals dc.CopyNumber into table2
                            from dc in table2.Distinct().ToList().Where(dc => dc.CopyNumber == l.CopyNumber).Distinct().ToList()
                            join dt in dvdTitle on dc.DVDNumber equals dt.DVDNumber into table3
                            from dt in table3.Distinct().ToList().Where(dt => dt.DVDNumber == dc.DVDNumber && l.DateReturned != DateTime.Parse("0001-01-01 00:00:00.0000000")).Distinct().ToList()
                            group new { l, m, dc, dt } by new { dt.DVDTitle, dc.CopyNumber, m.MemberFirstName, l.DateOut }
                           into grp
                            select new
                            {
                                TotalLoans = grp.Count(),
                                grp.Key.DVDTitle,
                                grp.Key.CopyNumber,
                                grp.Key.MemberFirstName,
                                grp.Key.DateOut,

                            }).OrderBy(X => X.TotalLoans);
            ViewBag.totalloans = copyloan;
            return View(copyloan);
        }
        
        //function 8
        
        
        //function 7
        public IActionResult LoanedDvdList()
        {
            
            DateTime currentDate = DateTime.Now.Date;
            DateTime lastDate = currentDate.Subtract(new TimeSpan(365, 0, 0, 0, 0));
            String d = "0";
            var loan = dataBaseContext.LoanModel?.ToList();
            var member = dataBaseContext.MemberModel?.ToList();
            var loanDetail = (from l in loan
                join m in member on l.MemberNumber equals m.MemberNumber into table1
                from m in table1.ToList().Where(m => m.MemberNumber == l.MemberNumber && l.DateReturned == DateTime.Parse("0001-01-01 00:00:00.0000000"))
                orderby l.CopyNumber ascending
                select new { loan = l, member = m });
            ViewBag.loanDetails = loanDetail;
            return View(loanDetail);
        }
        public IActionResult EditDvdCopyDetails(string CopyNumber)
        {
            //GET LOAN DETAILS OF THE COPY NUMBER
            ViewBag.UserLoanDetails = dataBaseContext.LoanModel.Where(l => l.LoanNumber == CopyNumber).First();
            var cop = ViewBag.UserLoanDetails;

            var copy = ViewBag.UserLoanDetails.CopyNumber;
            //GET DVD OF THE COPY NUMBER
            ViewBag.CopyDVDNumber = dataBaseContext.DVDCopyModel.Where(c => c.CopyNumber == CopyNumber).First();
            string copydvdnum = ViewBag.CopyDVDNumber.DVDNumber;

            //GET PENALTY CHARGE OF DVD NUMBER
            ViewBag.DVDNumber = dataBaseContext.DVDTitleModel.Where(d => d.DVDNumber == copydvdnum).First();
            ViewBag.PenaltyCharge = ViewBag.DVDNumber.PenaltyCharge;
            int pCharge = int.Parse(ViewBag.PenaltyCharge);

            //CALCULATING DATE OF RETURN
            DateTime dueDate = (ViewBag.UserLoanDetails.DateDue);
            DateTime returnDate = DateTime.Now.Date.Date;

            //GETTING ONLY DATE
            var onlydate = returnDate.ToShortDateString();

            //GETTING DAY DIFFERENCE
            TimeSpan difference = returnDate.Subtract(dueDate);
            int dueDay = difference.Days;

            ViewBag.ReturnDate = onlydate;
            if (dueDay < 0)
            {
                ViewBag.OverDue = "0";
                ViewBag.TotalCharge = "0";
            }
            else
            {
                ViewBag.OverDue = dueDay;
                int totalCharge = dueDay * pCharge;
                ViewBag.TotalCharge = totalCharge;
                Console.WriteLine(difference);
            }


            return RedirectToAction("LoanedDvdList");
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateReturnDVD
            (LoanModel loan, string loanNumber, string loantypenumber, string copynumber, int membernumber, DateTime dateOut, DateTime dateReturned, DateTime dateDue)
        {
            loan = dataBaseContext.LoanModel.Where(l => l.LoanTypeNumber == loantypenumber).First();

            loan.DateReturned = dateReturned;
            dataBaseContext.LoanModel.Update(loan);
            var result = await dataBaseContext.SaveChangesAsync();
            return RedirectToAction("LoanedDvdList");
        }
    
    }
    
    
    
}
    