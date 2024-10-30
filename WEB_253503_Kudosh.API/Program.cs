using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WEB_253503_Kudosh.API.Data;
using WEB_253503_Kudosh.API.Extensions;
using WEB_253503_Kudosh.API.Models;
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

var authServer = builder.Configuration
                        .GetSection("AuthServer")
                        .Get<AuthServerData>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
{
    o.MetadataAddress = $"{authServer.Host}/realms/{authServer.Realm}/.well-known/openid-configuration";
    o.Authority = $"{authServer.Host}/realms/{authServer.Realm}";
    o.Audience = "account";
    o.RequireHttpsMetadata = false;
});
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("admin", p => p.RequireRole("POWER-USER"));
});

var app = builder.Build();
await DbInitializer.SeedData(app);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
