namespace ASPNET_MVC.Models.VNPay
{
    //người dùng gửi tới server thông tin thanh toán
    public class PaymentInformationModel
    {
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
        public int CourseId {  get; set; }
        public int UserId { get; set; }
    }
}
