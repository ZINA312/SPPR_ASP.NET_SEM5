using Microsoft.EntityFrameworkCore;
using WEB_253503_Kudosh.API.Data;
using WEB_253503_Kudosh.API.Extensions;
using WEB_253503_Kudosh.API.Services.TelescopeCategoryService;
using WEB_253503_Kudosh.API.Services.TelescopeService;
using WEB_253503_Kudosh.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITelescopeService, TelescopeService>();

var app = builder.Build();
await DbInitializer.SeedData(app);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
