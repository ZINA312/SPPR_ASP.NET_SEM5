using Microsoft.EntityFrameworkCore;
using WEB_253503_Kudosh.API.Data;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;

namespace WEB_253503_Kudosh.API.Services.TelescopeCategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseData<List<CategoryEntity>>> GetCategoryListAsync()
        {
            var response = new ResponseData<List<CategoryEntity>>();
            response.Data = await _context.Categories.ToListAsync();
            return response;
        }
    }
}