using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;

namespace WEB_253503_Kudosh.UI.Services.TelescopeCategoryService
{
    public class MemoryCategoryService : ICategoryService
    {
        public Task<ResponseData<List<CategoryEntity>>> GetCategoryListAsync()
        {
            var categories = new List<CategoryEntity>
            {
                new(1, "Зеркальные", "reflectors"),
                new(2, "Линзовые", "refractors"),
                new(3, "Зеркально-Линзовые", "catadioptrics"),
            };
            var result = ResponseData<List<CategoryEntity>>.Success(categories);
            return Task.FromResult(result);
        }
    }
}
