using WEB_253503_Kudosh.API.Services;
using WEB_253503_Kudosh.API.Services.TelescopeCategoryService;
using WEB_253503_Kudosh.API.Services.TelescopeService;

namespace WEB_253503_Kudosh.API.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ITelescopeService, TelescopeService>();
        }
    }
}
