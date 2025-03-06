using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_API.Application.DTOs
{
    public class CategoryDTO
    {
    }

    public class CreateCategory
    {
        [Required]
        public string Name { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
    public class UpdateCategory
    {
        [Required]
        public int categoryId { get; set; }
        [Required]
        public string Name { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }

}
