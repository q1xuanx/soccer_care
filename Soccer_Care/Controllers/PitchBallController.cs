using Humanizer.Bytes;
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
            var pitch = _context.FootBallFields.ToList();
            foreach (FootBallFieldModel field in pitch)
            {
                field.ListField = _context.listFields.Where(i => i.IDFootballField == field.IDFootBallField).ToList();
                field.ratings = _context.Ratings.Where(i => i.IDField == field.IDFootBallField).ToList();
            }
            var getUserId = "803c37cd-1073-4508-9398-0e2ecf49a142";
            /*if (_userManager.GetUserAsync(HttpContext.User).Result.Id != null)
            {
                getUserId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            }*/
            if (getUserId != null)
            {
                ViewBag.ListFieldLike = _context.FieldLike.Where(i => i.Username == getUserId).ToList();
            }
            return View(pitch);
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
            var field= _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == id && i.isDisable == 0);
            if (field != null)
            {
                field.ListField = _context.listFields.Where(i => i.IDFootballField.Equals(field.IDFootBallField)).ToList();
                field.ratings = _context.Ratings.Where(i => i.IDField == field.IDFootBallField).ToList();
                foreach(RatingModel rate in field.ratings)
                {
                    rate.User = _context.User.FirstOrDefault(i => i.Id == rate.Username);
                }
                field.ratings.OrderByDescending(i => i.Diem).ToList();
                return View(field);
            }
            return NotFound();
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

        public async Task<IActionResult> Cod()
        {
            return RedirectToAction("Index", "Home");
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
        public bool CheckIsValid(String IDFootballField)
        {
            var idUser = _userManager.GetUserAsync(User).Result.Id;
            var isOrder = _context.HistoryOrders.Where(i => i.IDUser == idUser).Select(i => i.IDDetails).ToList();
            bool isValid = false;
            foreach (String dom in isOrder)
            {
                var getIDOrder = _context.DetailsOrder.FirstOrDefault(i => i.IDDetails == dom).IDOrder;
                var check = _context.OrderField.FirstOrDefault(i => i.IDOrder == getIDOrder).IDFootballField;
                if (check == IDFootballField)
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }
        [Authorize(Roles = "Admin,Partner,User")]
        public IActionResult AddComments(String message, int Points, String IDFootballField)
        {
            var idUser = _userManager.GetUserAsync(User).Result.Id;
            
            RatingModel ratingModel = new RatingModel()
            {
                IDDanhGia = Guid.NewGuid().ToString(),
                Diem = Points,
                Comments = message,
                Username = idUser,
                IDField = IDFootballField
            };
            _context.Ratings.Add(ratingModel);
            _context.SaveChanges();
            var getList = _context.Ratings.Where(i => i.IDField == IDFootballField).ToList();
            foreach (RatingModel rate in getList)
            {
                rate.User = _context.User.FirstOrDefault(i => i.Id == rate.Username);
            }
            return PartialView(getList.OrderByDescending(i => i.Diem).ToList());
        }
        [Authorize(Roles = "Admin,Partner,User")]
        public bool isLiked(string idFootballField)
        {
            var getID = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            FieldLikeModel isValid = _context.FieldLike.FirstOrDefault(i => i.IDFootballField.Equals(idFootballField) && i.Username.Equals(getID));
            if (isValid == null)
            {
                _context.FieldLike.Add(new FieldLikeModel()
                {
                    IDFieldLike = Guid.NewGuid().ToString(),
                    IDFootballField = idFootballField,
                    Username = getID
                });
                _context.SaveChanges();
                return true;
            }
            _context.FieldLike.Remove(isValid);
            _context.SaveChanges();
            return false;
        }
        public IActionResult removeComment(string idUser, string idDanhGia, string IDFootballField)
        {
            bool getRole = User.IsInRole("Admin");
            bool getPartner = User.IsInRole("Partner");
            if (getRole) {
                var findComment = _context.Ratings.FirstOrDefault(r => r.IDDanhGia == idDanhGia);
                if (findComment != null)
                {
                    _context.Ratings.Remove(findComment);
                    _context.SaveChanges();
                    var getList = _context.Ratings.Where(i => i.IDField == IDFootballField).ToList();
                    foreach (RatingModel rate in getList)
                    {
                        rate.User = _context.User.FirstOrDefault(i => i.Id == rate.Username);
                    }
                    return PartialView("AddComments", getList.OrderByDescending(i => i.Diem).ToList());
                }
                else
                {
                    return Content("Comment không tồn tại");
                }
            }else if (getPartner)
            {
                var checkIsOwner = _context.FootBallFields.FirstOrDefault(i => i.IDUserOwner == idUser && i.IDFootBallField == IDFootballField);
                if (checkIsOwner != null)
                {
                    var findComment = _context.Ratings.FirstOrDefault(r => r.IDDanhGia == idDanhGia);
                    if (findComment != null)
                    {
                        _context.Ratings.Remove(findComment);
                        _context.SaveChanges();
                        var getList = _context.Ratings.Where(i => i.IDField == IDFootballField).ToList();
                        foreach (RatingModel rate in getList)
                        {
                            rate.User = _context.User.FirstOrDefault(i => i.Id == rate.Username);
                        }
                        return PartialView("AddComments", getList.OrderByDescending(i => i.Diem).ToList());
                    }
                    else
                    {
                        return Content("Bạn không được xóa comment của người khác");
                    }
                }else
                {
                    return Content("Comment không tồn tại");
                }
            }
            else
            {
                var getId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
                var findComments = _context.Ratings.FirstOrDefault(i => i.Username == getId && i.IDDanhGia == idDanhGia);
                if (findComments != null)
                {
                    _context.Ratings.Remove(findComments);
                    _context.SaveChanges();
                    var getList = _context.Ratings.Where(i => i.IDField == IDFootballField).ToList();
                    foreach (RatingModel rate in getList)
                    {
                        rate.User = _context.User.FirstOrDefault(i => i.Id == rate.Username);
                    }
                    return PartialView("AddComments", getList.OrderByDescending(i => i.Diem).ToList());
                }else
                {
                    return Content("Bạn không được xóa comment của người khác");
                }
            }
        }
    }
}
