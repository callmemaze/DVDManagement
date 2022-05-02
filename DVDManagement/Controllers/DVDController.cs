using DVDManagement.Data;
using DVDManagement.Models;
using DVDManagement.ViewModel;
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

        public IActionResult SearchActor(string SearchString)
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
            if (!String.IsNullOrEmpty(SearchString))
            {
                var list = result.Where(x => x?.actor?.ActorSurname == (SearchString) || SearchString == null).ToList();
                return View(list);
            }
            else
            {
                return View(result);
            }
            
        }
        public IActionResult CopyNumberSearch(string SearchString)
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
            
            if (!String.IsNullOrEmpty(SearchString))
            {
                var list = result.Where(x => x?.loan?.CopyNumber == SearchString).ToList();
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

        public IActionResult LoanList()
        {
            var member = dataBaseContext!.MemberModel!.ToList();
            var loan = dataBaseContext!.LoanModel!.ToList();
            var dvdCopy = dataBaseContext!.DVDCopyModel!.ToList();
            var dvd = dataBaseContext!.DVDTitleModel!.ToList();

            var result = from d in dvdCopy 
                         join dvdTitle in dvd on d?.DVDNumber equals dvdTitle?.DVDNumber into table1
                         from t in table1.ToList()
                         join l in loan on d?.CopyNumber equals l?.CopyNumber into table2
                         from lt in table2.ToList()
                         join m in member on lt.MemberNumber equals m.MemberNumber into table3
                         from mt in table3.ToList()
                         select new LoanListViewModel
                         {
                             dvdCopy = d,
                             dvd = t,
                             loan = lt,
                             member = mt,
                         };
            var list = result.Where(x => x?.loan?.DateReturned == null).ToList();
            return View(list);
        }



    }
}
