using WEB_253503_Kudosh.UI.Models;
using WEB_253503_Kudosh.UI.Services.TelescopeCategoryService;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;
using WEB_253503_Kudosh.UI;
using WEB_253503_Kudosh.UI.Services.FileService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
var uriData = builder.Configuration.GetSection("UriData").Get<UriData>();
builder.Services.AddHttpClient<ITelescopeService, ApiTelescopeService>(opt =>opt.BaseAddress = new Uri(uriData.ApiUri));
builder.Services.AddHttpClient<ICategoryService, ApiTelescopeCategoryService>(opt => opt.BaseAddress = new Uri(uriData.ApiUri));
builder.Services.AddHttpClient<IFileService, ApiFileService>(opt => opt.BaseAddress = new Uri($"{uriData.ApiUri}Files"));
builder.Services.AddRazorPages();
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


app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "AdminArea",
    pattern: "AdminArea/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
