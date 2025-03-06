namespace ASPNET_API.Models.Momo;
//momo xác minh xong trả về chứa url QR
public class MomoCreatePaymentResponseModel
{
    public string RequestId { get; set; }
    public int ErrorCode { get; set; }
    public string OrderId { get; set; }
    public string Message { get; set; }
    public string LocalMessage { get; set; }
    public string RequestType { get; set; }
    public string PayUrl { get; set; }
    public string Signature { get; set; }
    public string QrCodeUrl { get; set; }
    public string Deeplink { get; set; }
    public string DeeplinkWebInApp { get; set; }
}