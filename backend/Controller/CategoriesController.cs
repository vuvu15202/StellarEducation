using ASPNET_API.Application.Interfaces;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASPNET_API.Application.Services;
using AutoMapper;
using ASPNET_API.Application.Services.Interfa;

namespace ASPNET_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly CategoryService _categoryService;


        public CategoriesController(IFileService fileService, IMapper mapper, CategoryService categoryService)
        {
            _fileService = fileService;
            _mapper = mapper;
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategory request)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(request);
            if (updatedCategory == null) return NotFound();
            return Ok(updatedCategory);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory([FromForm] CreateCategory request)
        {
            var category = await _categoryService.CreateCategoryAsync(request);
            if (category == null) return BadRequest("Lỗi khi tạo danh mục");
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var isDeleted = await _categoryService.DeleteCategoryAsync(id);
            if (!isDeleted) return NotFound();
            return Ok();
        }
    }
}
