using WEB_253503_Kudosh_Project.WEB_253503_Kudosh.UI.Services;
using WEB_253503_Kudosh_Project.WEB_253503_Kudosh.UI.Services.TelescopeCategoryService;
using WEB_253503_Kudosh_Project.WEB_253503_Kudosh.UI.Services.TelescopeProductService;

namespace WEB_253503_Kudosh_Project.WEB_253503_Kudosh.UI.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
            builder.Services.AddScoped<IProductService, MemoryProductService>();
        }
    }
}
