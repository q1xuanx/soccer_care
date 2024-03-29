using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;

namespace Soccer_Care.Controllers
{
    public class PitchBallController : Controller
    {
        IHttpContextAccessor contextAccessor;
        private readonly SoccerCareDbContext _context;
        public PitchBallController(IHttpContextAccessor contextAccessor, SoccerCareDbContext context) {
            this.contextAccessor = contextAccessor;
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DatSan(string id)
        {
            if (contextAccessor.HttpContext.Session.GetString("User") == null)
            {
                return RedirectToAction("Login", "User");
            }
            var pitch = _context.FootBallFields.FirstOrDefault(f => f.IDFootBallField == id);
            return View(pitch);
        }

        public IActionResult DS(string id)
        {
            var listField = _context.listFields.Where(l => l.IDFootballField == id).ToList();
            var pitch = _context.FootBallFields.FirstOrDefault(f => f.IDFootBallField == id);
            string namef = pitch.Name.ToString();
            ViewData["name"] = namef;
            return View(listField);
        }

        [HttpPost]
        public IActionResult DatSanConfirm(String id, String emailUser, String Username, String SDT, String BeginTime, DateTime Date)
        {

            /*            TimeSpan end = DateTime.Parse(BeginTime).AddHours(1).AddMinutes(30).TimeOfDay;
                        TimeSpan beforeBegin = DateTime.Parse(BeginTime).AddHours(-1).AddMinutes(-30).TimeOfDay;*/
            TimeSpan timeCurrent = DateTime.Parse(BeginTime).TimeOfDay;
            var getIDOrder = _context.OrderField.Where(i => i.IDFootballField == id).Select(i => i.IDOrder).ToList();
            if (getIDOrder != null) {
                foreach (var IdOrder in getIDOrder)
                {
                    var find = _context.DetailsOrder.FirstOrDefault(i => i.IDOrder == IdOrder);
                    if (find.DateTime == Date)
                    {

                        var getBeforeBegin = DateTime.Parse(find.StartTime).AddHours(-1).AddMinutes(-30).TimeOfDay;
                        var getAfterBegin = DateTime.Parse(find.StartTime).AddHours(1).AddMinutes(30).TimeOfDay;
                        if ((timeCurrent < getAfterBegin && timeCurrent > getBeforeBegin))
                        {
                            return Content("Loi dat san");
                        }
                    }
                    else continue;
                }
            }
            OrderFieldModel order = new OrderFieldModel();
            order.IDOrder = Guid.NewGuid().ToString();
            order.IDFootballField = id;
            order.Username = Username;
            order.SoDienThoai = SDT;

            DetailsOrderModel details = new DetailsOrderModel();
            details.IDOrder = order.IDOrder;
            details.IDDetails = Guid.NewGuid().ToString();
            details.StartTime = BeginTime; 
            details.DateTime = Date;

            HistoryOrderModel history = new HistoryOrderModel();
            history.IDFootballField = id;
            history.Username = Username;
            history.IDHistory = Guid.NewGuid().ToString();

            _context.OrderField.Add(order);
            _context.DetailsOrder.Add(details);
            _context.HistoryOrders.Add(history);

            _context.SaveChanges();
            return View("Thanks");
        }
    }
}
