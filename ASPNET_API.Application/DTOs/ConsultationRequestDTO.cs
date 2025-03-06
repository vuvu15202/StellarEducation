using System.ComponentModel.DataAnnotations;

namespace ASPNET_API.Application.DTOs
{
    public class ConsultationRequestDTO
    {
        public int? ConsultationRequestId { get; set; } = default(int?);
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedAtString { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên.")]
        public string ContactName { get; set; }

        [RegularExpression(@"^(|[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,})$", ErrorMessage = "Email sai định dạng.")]
        public string Email { get; set; }

        [RegularExpression(@"^(0|\+84)(3[2-9]|5[6|8|9]|7[0|6|7|8|9]|8[1-5]|9[0-4|6-9])\d{7}$", ErrorMessage = "Số điện thoại sai định dạng, 0912345678 hoặc +84356789123.")]
        public string PhoneNumber { get; set; }
        public string? Message { get; set; }
        public bool IsResolved { get; set; } = false;
        public int? ResolvedById { get; set; }

        public virtual UserDTO? ResolvedBy { get; set; }
    }
}
