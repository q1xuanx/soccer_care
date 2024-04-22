using Humanizer;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Soccer_Care.Models;
using Soccer_Care.Services;
using System.Reflection.Metadata;
using System.Xml.Linq;


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
            if (contextAccessor.HttpContext.Session.GetString("price1") != null)
            {
                var price1 = contextAccessor.HttpContext.Session.GetString("price1");
                var price2 = contextAccessor.HttpContext.Session.GetString("price2");
                var address = contextAccessor.HttpContext.Session.GetString("address");
                List<FootBallFieldModel> pitchTemp = _context.FootBallFields.ToList();
                List<FootBallFieldModel> pitch1 = new List<FootBallFieldModel>();
                foreach (var item in pitchTemp)
                {
                    if (item.Address.Contains(address))
                    {
                        if (item.Gia >= float.Parse(price1) && item.Gia <= float.Parse(price2))
                        {
                            pitch1.Add(item);
                        }
                    }
                }
                foreach (FootBallFieldModel field in pitch1)
                {
                    field.ListField = _context.listFields.Where(i => i.IDFootballField == field.IDFootBallField).ToList();
                    field.ratings = _context.Ratings.Where(i => i.IDField == field.IDFootBallField).ToList();
                }
                var getUserId1 = _userManager.GetUserAsync(HttpContext.User);
                if (getUserId1.Result == null) return View("ListPitchComponent", pitch1);
                ViewBag.ListFieldLike = _context.FieldLike.Where(i => i.Username == getUserId1.Result.Id).ToList();
                contextAccessor.HttpContext.Session.Remove("price1");
                contextAccessor.HttpContext.Session.Remove("price2");
                contextAccessor.HttpContext.Session.Remove("address");
                return View(pitch1);
            }
            var pitch = _context.FootBallFields.Where(i => i.isDisable == 0).ToList();
            foreach (FootBallFieldModel field in pitch)
            {
                field.ListField = _context.listFields.Where(i => i.IDFootballField == field.IDFootBallField).ToList();
                field.ratings = _context.Ratings.Where(i => i.IDField == field.IDFootBallField).ToList();
            }
            var getUserId = _userManager.GetUserAsync(HttpContext.User);
            if (getUserId.Result == null) return View("Index", pitch);
            ViewBag.ListFieldLike = _context.FieldLike.Where(i => i.Username == getUserId.Result.Id).ToList();
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
            var field = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == id && i.isDisable == 0);
            if (field != null)
            {
                field.ListField = _context.listFields.Where(i => i.IDFootballField.Equals(field.IDFootBallField)).ToList();
                foreach(ListFieldModel fields in field.ListField)
                {
                    fields.Type = _context.TypeFields.FirstOrDefault(i => i.IDType == fields.IDType);
                }
                field.ratings = _context.Ratings.Where(i => i.IDField == field.IDFootBallField).ToList();
                foreach (RatingModel rate in field.ratings)
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
        public async Task<IActionResult> DatSanConfirm(String id, String emailUser, String Username, String SDT, String BeginTime, DateTime Date, String idParentField)
        {
            
            TimeSpan timeCurrent = DateTime.Parse(BeginTime).TimeOfDay;
            DateTime futureTime = DateTime.Parse(BeginTime).AddHours(1).AddMinutes(30);
            TimeSpan timeDiff = timeCurrent - DateTime.Now.TimeOfDay;

            if (timeCurrent.Hours >= 21 || futureTime.Hour >= 21 || timeCurrent.Hours <= 7)
            {
                TempData["LoiDatSan"] = "Quá giờ đặt sân";
                var pitch = _context.listFields.FirstOrDefault(f => f.IDField == id);
                pitch.FootBall = _context.FootBallFields.Where(f => f.IDFootBallField == pitch.IDFootballField).FirstOrDefault();
                return View("DatSan", pitch);
            }else if (Math.Abs(timeDiff.TotalMinutes) <= 30)
            {
                TempData["LoiDatSan"] = "Giờ đặt sân không hợp lệ vui lòng đặt sân sớm hơn 30 phút";
                var pitch = _context.listFields.FirstOrDefault(f => f.IDField == id);
                pitch.FootBall = _context.FootBallFields.Where(f => f.IDFootBallField == pitch.IDFootballField).FirstOrDefault();
                return View("DatSan", pitch);
            }
            var getIDOrder = _context.OrderField.Where(i => i.IDFootballField == idParentField && i.IDChildField == id).Select(i => i.IDOrder).ToList();
            if (getIDOrder != null) {
                foreach (var IdOrder in getIDOrder)
                {
                    var find = _context.DetailsOrder.FirstOrDefault(i => i.IDOrder == IdOrder);
                    if (find.DateTime == Date && find.Status == "Đang Đến")
                    {
                        var getBeforeBegin = DateTime.Parse(find.StartTime).AddHours(-1).AddMinutes(-30).TimeOfDay;
                        var getAfterBegin = DateTime.Parse(find.StartTime).AddHours(1).AddMinutes(30).TimeOfDay;
                        if ((timeCurrent < getAfterBegin && timeCurrent > getBeforeBegin) || (DateTime.Parse(BeginTime) == DateTime.Parse(find.StartTime)) || DateTime.Parse(BeginTime) >= DateTime.Parse(find.StartTime) && DateTime.Parse(BeginTime) <= DateTime.Parse(find.StartTime).AddHours(1).AddMinutes(30))
                        {
                            TempData["LoiDatSan"] = "Giờ Đặt Sân Không Hợp Lệ, Đã có người đặt sân trong khung giờ này";
                            var pitch = _context.listFields.FirstOrDefault(f => f.IDField == id);
                            pitch.FootBall = _context.FootBallFields.Where(f => f.IDFootBallField == pitch.IDFootballField).FirstOrDefault();
                            return View("DatSan", pitch);
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
            details.Status = "Đang Đến";



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
        public IActionResult CancelOrder(string id)
        {
            var order = _context.DetailsOrder.FirstOrDefault(i => i.IDOrder == id);
            DateTime startTime = DateTime.Parse(order.StartTime);
            DateTime currentTime = DateTime.Now;
            TimeSpan timeDifference = startTime -  currentTime ;
            if (timeDifference.TotalMinutes <= 30 && timeDifference.TotalMinutes >= 0)
            {
                return Content("Quá thời gian hủy sân");
            }
            else
            {
                order.Status = "Đã Hủy";
                var getUser = _userManager.GetUserAsync(HttpContext.User).Result;
                if (getUser == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                _context.DetailsOrder.Update(order);
                _context.SaveChanges();
                List<HistoryOrderModel> hisOrder = _context.HistoryOrders.Where(i => i.IDUser == getUser.Id).ToList();
                foreach (HistoryOrderModel his in hisOrder)
                {
                    his.DetailsOrder = _context.DetailsOrder.Where(i => i.IDDetails.Equals(his.IDDetails)).FirstOrDefault();
                    his.DetailsOrder.Order = _context.OrderField.Where(i => i.IDOrder.Equals(his.DetailsOrder.IDOrder)).FirstOrDefault();
                    his.DetailsOrder.Order.FootBall = _context.FootBallFields.FirstOrDefault(i => i.IDFootBallField == his.DetailsOrder.Order.IDFootballField);
                }
                return PartialView("CancelOrder", hisOrder);
            }
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
