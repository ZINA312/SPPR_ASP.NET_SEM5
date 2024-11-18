using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;

namespace WEB_253503_Kudosh.BlazorWasm.Services.DataService
{
    public interface IDataService
    {
        event Action DataLoaded;
        List<CategoryEntity> Categories { get; set; }
        List<TelescopeEntity> Telescopes { get; set; }
        bool Success { get; set; }
        string ErrorMessage { get; set; }
        int TotalPages { get; set; }
        int CurrentPage { get; set; }
        CategoryEntity SelectedCategory { get; set; }

        public Task GetProductListAsync(int pageNo = 1);

        public Task GetCategoryListAsync();
    }
}
