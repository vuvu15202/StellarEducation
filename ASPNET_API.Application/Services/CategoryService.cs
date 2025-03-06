using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNET_API.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET_API.Application.DTOs;
using ASPNET_API.Infrastructure.Data;
using ASPNET_API.Application.Services.Interfa;

namespace ASPNET_API.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DonationWebApp_v2Context _context;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CategoryService(DonationWebApp_v2Context context, IFileService fileService, IMapper mapper)
        {
            _context = context;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category?> UpdateCategoryAsync(UpdateCategory request)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == request.categoryId);
            if (category == null) return null;

            _mapper.Map(request, category);

            if (request.ImageFile != null)
            {
                var imageFile = await _fileService.SaveImageAsync(request.ImageFile);
                if (imageFile.status == 0) return null;

                if (imageFile.status == 1)
                {
                    await _fileService.DeleteImageAsync(category.Image);
                    category.Image = imageFile.message;
                }
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }

            return category;
        }

        public async Task<Category?> CreateCategoryAsync(CreateCategory request)
        {
            if (request.ImageFile == null)
            {
                return null;
            }

            var category = _mapper.Map<Category>(request);
            var file = await _fileService.SaveImageAsync(request.ImageFile);
            if (file.status == 0) return null;

            if (file.status == 1)
            {
                category.Image = file.message;
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            category.IsDelete = true;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
