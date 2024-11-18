using System.Collections.Generic;
using System.Diagnostics;
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
            if (pageNo > dataList.TotalPages)
                return ResponseData<ListModel<TelescopeEntity>>.Error("No such page");
            dataList.CurrentPage = pageNo;
            return ResponseData<ListModel<TelescopeEntity>>.Success(dataList);
        }

        public async Task<ResponseData<TelescopeEntity>> GetProductByIdAsync(int id)
        {
            var response = new ResponseData<TelescopeEntity>();
            var product = await _context.Telescopes.FindAsync(id);
            return ResponseData<TelescopeEntity>.Success(product);
        }

        public async Task<ResponseData<bool>> UpdateProductAsync(int id, TelescopeEntity product)
        {
            if (product == null || id != product.Id)
            {
                return ResponseData<bool>.Success(false);
            }

            var existingProduct = await _context.Telescopes.FindAsync(id);
            if (existingProduct == null)
            {
                return ResponseData<bool>.Success(false);
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Price = product.Price;
            existingProduct.ImagePath = product.ImagePath ?? existingProduct.ImagePath;
            
            _context.Telescopes.Update(existingProduct);
            await _context.SaveChangesAsync();
            return ResponseData<bool>.Success(true);
        }

        public async Task<ResponseData<bool>> DeleteProductAsync(int id)
        {
            var product = await _context.Telescopes.FindAsync(id);
            if (product != null)
            {
                _context.Telescopes.Remove(product);
                await _context.SaveChangesAsync();
                return ResponseData<bool>.Success(true);
            }
            else
            {
                return ResponseData<bool>.Error("Product not found.");
            }
        }

        public async Task<ResponseData<TelescopeEntity>> CreateProductAsync(TelescopeEntity product)
        {
            if (product == null)
            {
                return ResponseData<TelescopeEntity>.Error("Product cannot be null.");
            }

            product.MimeType = "";
            await _context.Telescopes.AddAsync(product);
            await _context.SaveChangesAsync();

            return ResponseData<TelescopeEntity>.Success(product);
        }

       
    }
}