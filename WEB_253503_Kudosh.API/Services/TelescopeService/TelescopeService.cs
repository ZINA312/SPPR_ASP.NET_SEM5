using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WEB_253503_Kudosh.API.Data;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;

namespace WEB_253503_Kudosh.API.Services.TelescopeService
{
    public class TelescopeService : ITelescopeService
    {
        private readonly AppDbContext _context;
        private readonly int _maxPageSize = 20;
        public TelescopeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseData<ListModel<TelescopeEntity>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
        {
            if (pageSize > _maxPageSize) pageSize = _maxPageSize;
            var dataList = new ListModel<TelescopeEntity>();
            var query = _context.Telescopes.AsQueryable();

            if (!string.IsNullOrEmpty(categoryNormalizedName))
            {
                query = query.Where(t => t.Category.NormalizedName == categoryNormalizedName);
            }

            var totalCount = await query.CountAsync();
            dataList.Items = query.OrderBy(d => d.Id)
                                    .Skip((pageNo - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();
            dataList.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            dataList.CurrentPage = pageNo;

            return ResponseData<ListModel<TelescopeEntity>>.Success(dataList);
        }

        public async Task<ResponseData<TelescopeEntity>> GetProductByIdAsync(int id)
        {
            var response = new ResponseData<TelescopeEntity>();
            var product = await _context.Telescopes.FindAsync(id);

            return ResponseData<TelescopeEntity>.Success(product);
        }

        public async Task UpdateProductAsync(int id, TelescopeEntity product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<TelescopeEntity>> CreateProductAsync(TelescopeEntity product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            throw new NotImplementedException();
        }
    }
}