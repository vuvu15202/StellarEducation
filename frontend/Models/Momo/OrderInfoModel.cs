namespace ASPNET_MVC.Models.Momo;
//người dùng gửi tới server thông tin thanh toán
public class OrderInfoModel
{
    public string FullName { get; set; }
    public string OrderId { get; set; }
    public string OrderInfo { get; set; }
    public double Amount { get; set; }
    public int CourseId { get; set; }
    public int UserId { get; set; }

}