using WEB_253503_Kudosh_Project.WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh_Project.WEB_253503_Kudosh.Domain.Models;
using WEB_253503_Kudosh_Project.WEB_253503_Kudosh.UI.Services.TelescopeCategoryService;

namespace WEB_253503_Kudosh_Project.WEB_253503_Kudosh.UI.Services.TelescopeProductService
{
    public class MemoryProductService : IProductService
    {

        List<TelescopeEntity> _telescopes;
        List<CategoryEntity> _categories;
                public MemoryProductService(ICategoryService categoryService)
                {
                    _categories = categoryService.GetCategoryListAsync().Result.Data;
                    SetupData();
                }
                private void SetupData()
                {
                    _telescopes = new List<TelescopeEntity>
                    {
                        new TelescopeEntity(1, "Sky-Watcher 150/750", "Отличный телескоп для новичков", _categories.Find(c => c.NormalizedName.Equals("reflectors")), 1000, "Images/SkyWatcher15075.jpg"),
                        new TelescopeEntity(2, "Celestron AstroMaster 70AZ", "Легкий и портативный рефрактор", _categories.Find(c => c.NormalizedName.Equals("refractors")), 800, "Images/AstroMaster70AZ.jpeg"),
                        new TelescopeEntity(3, "Meade LX90 8-inch", "Катадиоптрический телескоп с отличной оптикой", _categories.Find(c => c.NormalizedName.Equals("catadioptrics")), 2000, "Images/LX90.jpg"),
                        new TelescopeEntity(4, "Orion SkyQuest XT8", "Надежный рефлектор для наблюдений за глубоким небом", _categories.Find(c => c.NormalizedName.Equals("reflectors")), 1200, "Images/SkyQuestXT8.jpg"),
                        new TelescopeEntity(5, "Celestron NexStar 6SE", "Комбинированный телескоп с автонаведением", _categories.Find(c => c.NormalizedName.Equals("catadioptrics")), 1500, "Images/NexStar6SE.jpg"),
                        new TelescopeEntity(6, "William Optics Zenithstar 73", "Премиум рефрактор для астрономов", _categories.Find(c => c.NormalizedName.Equals("refractors")), 1700, "Images/Zenithstar73.jpg")
                    };
                }

        public Task<ResponseData<TelescopeEntity>> CreateProductAsync(TelescopeEntity product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<TelescopeEntity>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ListModel<TelescopeEntity>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {

            const int pageSize = 5;

            var filteredTelescopes = string.IsNullOrEmpty(categoryNormalizedName)
                ? _telescopes
                : _telescopes.Where(t => t.Category.NormalizedName.Equals(categoryNormalizedName, StringComparison.OrdinalIgnoreCase)).ToList();

            var totalCount = filteredTelescopes.Count;

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var paginatedTelescopes = filteredTelescopes
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var responseData = new ResponseData<ListModel<TelescopeEntity>>
            {
                Data = new ListModel<TelescopeEntity>
                {
                    Items = paginatedTelescopes,
                    CurrentPage = pageNo,
                    TotalPages = totalPages
                }
            };

            return Task.FromResult(responseData);
        }

        public Task UpdateProductAsync(int id, TelescopeEntity product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
