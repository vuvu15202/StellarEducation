using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ASPNET_MVC.Models.Entity;
using ASPNET_MVC.Models.VNPay;
using ASPNET_MVC.Services;
using ASPNET_MVC.temp;

namespace ASPNET_MVC.Controllers.Payment
{
    public class VNPayController : Controller
    {
        private readonly IVnPayService _vnPayService;

        public VNPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var user = (UserLoggedIn)HttpContext.Items["UserLoggedIn"];
            model.UserId = user.UserInfo.UserId;
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public async Task<IActionResult> PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            //DonationWebApp_v2Context _context = new DonationWebApp_v2Context();

            //try
            //{
            //    StudentFee studentFee = new StudentFee()
            //    {
            //        //CourseEnrollId = int.Parse(response.OrderDescription.Split(";")[1]),
            //        StudentFeeId = response.OrderId,
            //        PaymentMethod = response.PaymentMethod,
            //        BankCode = response.Vnp_BankCode,
            //        Amount = response.Vnp_Amount,
            //        OrderInfo = response.OrderDescription,
            //        ErrorCode = response.VnPayResponseCode,
            //        LocalMessage = response.VnPayResponseCode.Equals("00") ? "Thành Công" : "Đang xử lí",
            //        DateOfPaid = DateTime.Now,
            //    };
            //    _context.StudentFees.Add(studentFee);
            //    _context.SaveChanges();

            //    CourseEnroll courseEnroll = new CourseEnroll()
            //    {
            //        UserId = response.UserId,
            //        CourseId = response.CourseId,
            //        EnrollDate = DateTime.Now,
            //        ExpireDate = DateTime.Now.AddMonths(3),
            //        LessonCurrent = 1,
            //        CourseStatus = 0,
            //        StudentFeeId = studentFee.StudentFeeId,
            //    };
            //    _context.CourseEnrolls.Add(courseEnroll);
            //    _context.SaveChanges();

            //    //update studentfee
            //    studentFee.CourseEnrollId = courseEnroll.CourseEnrollId;
            //    _context.StudentFees.Update(studentFee);
            //    _context.SaveChanges();

            //    var user = (User)HttpContext.Items["User"];
            //    if (response.VnPayResponseCode.Equals("00"))
            //    {
            //        await _emailSender.SendEmailAsync(user.Email, "Đăng ký khóa học thành công", $"Bạn vừa đăng ký khóa học thành công trên UnitCat, Hãy bắt đầu học ngay nào!");
            //    }
            //    else
            //    {
            //        await _emailSender.SendEmailAsync(user.Email, "Đăng ký khóa học thành công", $"Bạn vừa đăng ký khóa học không thành công trên UnitCat, hãy kiểm tra lại giao dịch của bạn hoặc liên hệ với chúng tôi qua hotline hỗ trợ: .....");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return Redirect($"/Courses/Detail?courseId={response.CourseId}&statuscode=2");

            //}
            //return Redirect($"/Courses/lesson?courseId={response.CourseId}&lessonNum=1&statuscode=1");
            return Redirect($"/Courses/Detail?courseId={response.CourseId}&statuscode=1");

        }



    }
}

//{
//    "orderDescription": "Code Mega Thanh toán tại Code Mega 20000000",
//    "transactionId": "14302058",
//    "orderId": "638440657783943347",
//    "paymentMethod": "VnPay",
//    "paymentId": "14302058",
//    "success": true,
//    "token": "ccfb6ac10b1cec317ee1d39e32b4567acc8a0a45db00fd1ab",
//    "vnPayResponseCode": "00"
//}