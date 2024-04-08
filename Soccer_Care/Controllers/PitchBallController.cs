using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;
using Soccer_Care.Services;


namespace Soccer_Care.Controllers
{
 
    public class PitchBallController : Controller
    {
        IHttpContextAccessor contextAccessor;
        private readonly SoccerCareDbContext _context;
        private readonly UserManager<UserModel> _userManager;
		private readonly IVnPayService _vnPayService;
		public PitchBallController(IHttpContextAccessor contextAccessor, SoccerCareDbContext context, UserManager<UserModel> userManager, IVnPayService vnPayService) {
            this.contextAccessor = contextAccessor;
            this._context = context;
            this._userManager = userManager;
			_vnPayService = vnPayService;
		}
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Partner,User")]
        public IActionResult DatSan(string idField)
        {
            var pitch = _context.listFields.FirstOrDefault(f => f.IDField == idField);
            pitch.FootBall = _context.FootBallFields.Where(f => f.IDFootBallField == pitch.IDFootballField).FirstOrDefault();
            return View(pitch);
        }
        [Authorize(Roles = "Admin,Partner,User")]
        public IActionResult DS(string id)
        {
            var listField = _context.listFields.Where(l => l.IDFootballField == id).ToList();
            var pitch = _context.FootBallFields.FirstOrDefault(f => f.IDFootBallField == id);
            string namef = pitch.Name.ToString();
            string addressf = pitch.Address.ToString();
            ViewData["name"] = namef;
            ViewData["address"] = addressf;
            return View(listField);
        }
        [Authorize(Roles = "Admin,Partner,User")]
        [HttpPost]
        public async Task<IActionResult> DatSanConfirm(String id, String emailUser, String Username,String SDT, String BeginTime, DateTime Date, String idParentField)
        {
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
            order.IDFootballField = idParentField;
            order.IDUser = _userManager.FindByNameAsync(emailUser).Result.Id;
            order.SoDienThoai = SDT;
            order.IDChildField = id;


            DetailsOrderModel details = new DetailsOrderModel();
            details.IDOrder = order.IDOrder;
            details.IDDetails = Guid.NewGuid().ToString();
            details.StartTime = BeginTime; 
            details.DateTime = Date;



            HistoryOrderModel history = new HistoryOrderModel();
            history.IDDetails = details.IDDetails;
            history.Username = Username;
            history.IDHistory = Guid.NewGuid().ToString();
            history.IDUser = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;

            _context.OrderField.Add(order);
            _context.DetailsOrder.Add(details);
            _context.HistoryOrders.Add(history);

            _context.SaveChanges();
            return View("Thanks", details);
        }
        public async Task<IActionResult> HistoryOrder(String name)
        {
            List<HistoryOrderModel> hisOrder = _context.HistoryOrders.Where(i => i.IDUser == name).ToList();
            foreach(HistoryOrderModel his in hisOrder)
            {
                his.DetailsOrder = _context.DetailsOrder.Where(i => i.IDDetails.Equals(his.IDDetails)).FirstOrDefault();
                his.DetailsOrder.Order = _context.OrderField.Where(i => i.IDOrder.Equals(his.DetailsOrder.IDOrder)).FirstOrDefault();
                his.DetailsOrder.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == his.DetailsOrder.Order.IDFootballField);
            }
            return View(hisOrder);
        }
		public async Task<IActionResult> Vnpay(string id, string order, string detail)
		{
            var orderModel = _context.OrderField.FirstOrDefault(o => o.IDOrder == order);
            var childFiled = _context.listFields.FirstOrDefault(x => x.IDField == orderModel.IDChildField);
            
			var vnPayModel = new VnPaymentRequestModel
			{
				Amount = childFiled.Gia,
				CreatedDate = DateTime.Now,
				Description = $"Minh Chien 0392845906",
				FullName = "Nguyen Minh Chien",
				OrderId = new Random().Next(1000, 100000)
			};

			var detailModel = _context.DetailsOrder.FirstOrDefault(d => d.IDDetails == detail);
			if(detailModel != null)
            {
				detailModel.isThanhToan = 1;
			}
		    _context.DetailsOrder.Update(detailModel);
            _context.SaveChanges();
			return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));

		}
		public IActionResult PaymentSuccess()
		{
			return View("ThanksPayment");
		}

		public IActionResult PaymentFail()
		{
			return View("PaymentFaild");
		}


		public IActionResult PaymentCallBack()
		{
			var response = _vnPayService.PaymentExecute(Request.Query);

			if (response == null || response.VnPayResponseCode != "00")
			{
				TempData["Message"] = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}";
				return RedirectToAction("PaymentFail");
			}


			// Lưu đơn hàng vô database


			TempData["Message"] = $"Thanh toán VNPay thành công";
			return RedirectToAction("PaymentSuccess");
		}
	}
}
