using WEB_253503_Kudosh_Project.WEB_253503_Kudosh.Domain.Models;
using WEB_253503_Kudosh_Project.WEB_253503_Kudosh.Domain.Entities;

namespace WEB_253503_Kudosh_Project.WEB_253503_Kudosh.UI.Services.TelescopeCategoryService
{
    public interface ICategoryService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        /// <returns></returns>
        public Task<ResponseData<List<CategoryEntity>>> GetCategoryListAsync();
    }
}
