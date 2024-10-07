using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;

namespace WEB_253503_Kudosh.API.Services.TelescopeCategoryService
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<CategoryEntity>>> GetCategoryListAsync();
    }
}
