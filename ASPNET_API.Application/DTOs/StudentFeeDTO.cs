using ASPNET_API.Models.DTO;

namespace ASPNET_API.Application.DTOs
{
    public class StudentFeeDTO
    {
        public string StudentFeeId { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;
        public string? BankCode { get; set; }
        public string Amount { get; set; } = null!;
        public string OrderInfo { get; set; } = null!;
        public string ErrorCode { get; set; } = null!;
        public string LocalMessage { get; set; } = null!;
        public string? DateOfPaid { get; set; }

        public virtual CourseDTO Course { get; set; } = null!;

    }
}
