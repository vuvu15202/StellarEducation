using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET_API.Models.DTO;
using ASPNET_API.Models.Entity;
using ASPNET_API.Services;
using ASPNET_API.temp;

namespace ASPNET_API.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;


        public CategoriesController(DonationWebApp_v2Context context, IFileService fileService, IMapper mapper)
        {
            _context = context;
            _fileService = fileService;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategory request)
        {

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == request.categoryId);
            if(category == null) return NotFound();

            _mapper.Map(request, category);
            if (request.ImageFile != null)
            {
                var ImageFile = await _fileService.SaveImageAsync(request.ImageFile);
                if (ImageFile.status == 0) return Conflict(ImageFile.message);
                if (ImageFile.status == 1)
                {
                    await _fileService.DeleteImageAsync(category.Image);
                    category.Image = ImageFile.message;
                }
            }
                
            

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(request.categoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(category);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromForm] CreateCategory request)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'DonationWebApp_v2Context.Categories'  is null.");
            }
            if(request.ImageFile != null)
            {
                var category = _mapper.Map<Category>(request);
                var file = await _fileService.SaveImageAsync(request.ImageFile);
                if (file.status == 0) return NotFound(file.message);
                if (file.status == 1) category.Image = file.message;
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            else
            {
                return BadRequest("File ảnh bị trống");
            }
            

            
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            category.IsDelete = true;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
