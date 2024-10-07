using WEB_253503_Kudosh_Project.WEB_253503_Kudosh.Domain.Models;
using WEB_253503_Kudosh_Project.WEB_253503_Kudosh.Domain.Entities;

namespace WEB_253503_Kudosh_Project.WEB_253503_Kudosh.UI.Services.TelescopeProductService
{
    public interface IProductService
    {
        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <param name="categoryNormalizedName">нормализованное имя категории дляфильтрации</param>
        /// <param name="pageNo">номер страницы списка</param>
        /// <returns></returns>
    public Task<ResponseData<ListModel<TelescopeEntity>>> GetProductListAsync(string?
    categoryNormalizedName, int pageNo = 1);
            /// <summary>
            /// Поиск объекта по Id
            /// </summary>
            /// <param name="id">Идентификатор объекта</param>
            /// <returns>Найденный объект или null, если объект не найден</returns>
            public Task<ResponseData<TelescopeEntity>> GetProductByIdAsync(int id);
            /// <summary>
            /// Обновление объекта
            /// </summary>
            /// <param name="id">Id изменяемомго объекта</param>
            /// <param name="product">объект с новыми параметрами</param>
            /// <param name="formFile">Файл изображения</param>
            /// <returns></returns>
            public Task UpdateProductAsync(int id, TelescopeEntity product, IFormFile? formFile);
            /// <summary>
            /// Удаление объекта
            /// </summary>
            /// <param name="id">Id удаляемомго объекта</param>
            /// <returns></returns>
            public Task DeleteProductAsync(int id);
            /// <summary>
            /// Создание объекта
            /// </summary>
            /// <param name="product">Новый объект</param>
            /// <param name="formFile">Файл изображения</param>
            /// <returns>Созданный объект</returns>
            public Task<ResponseData<TelescopeEntity>> CreateProductAsync(TelescopeEntity product, IFormFile?
            formFile);
    }
}
