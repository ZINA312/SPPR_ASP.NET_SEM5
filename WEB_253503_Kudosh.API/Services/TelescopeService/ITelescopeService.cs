using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;

namespace WEB_253503_Kudosh.API.Services.TelescopeService
{
    public interface ITelescopeService
    {

        public Task<ResponseData<ListModel<TelescopeEntity>>> GetProductListAsync(string?
        categoryNormalizedName, int pageNo = 1, int pageSize = 3);

        public Task<ResponseData<TelescopeEntity>> GetProductByIdAsync(int id);

        public Task UpdateProductAsync(int id, TelescopeEntity product, IFormFile? formFile);

        public Task DeleteProductAsync(int id);

        public Task<ResponseData<TelescopeEntity>> CreateProductAsync(TelescopeEntity product, IFormFile? formFile);

        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile);
    }
}
