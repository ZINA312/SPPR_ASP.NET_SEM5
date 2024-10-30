using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.UI.Models;
using WEB_253503_Kudosh.UI.Services.Authentication;
using WEB_253503_Kudosh.UI.Services.CartService;
using WEB_253503_Kudosh.UI.Services.FileService;
using WEB_253503_Kudosh.UI.Services.TelescopeCategoryService;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;

namespace WEB_253503_Kudosh.UI.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            var uriData = builder.Configuration.GetSection("UriData").Get<UriData>();
            builder.Services.AddHttpClient<ITelescopeService, ApiTelescopeService>(opt => opt.BaseAddress = new Uri(uriData.ApiUri));
            builder.Services.AddHttpClient<ICategoryService, ApiTelescopeCategoryService>(opt => opt.BaseAddress = new Uri(uriData.ApiUri));
            builder.Services.AddHttpClient<IFileService, ApiFileService>(opt => opt.BaseAddress = new Uri($"{uriData.ApiUri}Files"));
            builder.Services.AddHttpClient<ITokenAccessor, KeycloakTokenAccessor>();
            builder.Services.AddHttpClient<IAuthService, KeycloakAuthService>();
            builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
        }
    }
}
