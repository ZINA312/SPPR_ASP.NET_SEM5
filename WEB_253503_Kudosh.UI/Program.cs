using WEB_253503_Kudosh.UI.Models;
using WEB_253503_Kudosh.UI.Services.TelescopeCategoryService;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;
using WEB_253503_Kudosh.UI;
using WEB_253503_Kudosh.UI.Services.FileService;
using WEB_253503_Kudosh.UI.HelperClasses;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using WEB_253503_Kudosh.UI.Extensions;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

HostingExtensions.RegisterCustomServices(builder);

builder.Services.AddRazorPages();

builder.Services.Configure<KeycloakData>(builder.Configuration.GetSection("Keycloak"));
var keycloakData = builder.Configuration.GetSection("Keycloak").Get<KeycloakData>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =
    OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddJwtBearer()
.AddOpenIdConnect(options =>
{
    options.Authority =
    $"{keycloakData.Host}/auth/realms/{keycloakData.Realm}";
    options.ClientId = keycloakData.ClientId;
    options.ClientSecret = keycloakData.ClientSecret;
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.Scope.Add("openid"); 
    options.SaveTokens = true;
    options.RequireHttpsMetadata = false; 
options.MetadataAddress = $"{keycloakData.Host}/realms/{keycloakData.Realm}/.well-known/openid-configuration";
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "AdminArea",
    pattern: "AdminArea/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
