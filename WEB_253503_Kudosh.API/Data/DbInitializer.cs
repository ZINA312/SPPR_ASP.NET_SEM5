using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.API.Data;
using Microsoft.EntityFrameworkCore;

namespace WEB_253503_Kudosh.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();

            await context.Database.EnsureCreatedAsync();

  
            if (context.Telescopes.Any())
            {
                return; 
            }

            var categories = new List<CategoryEntity>
            {
                new() { Name = "Зеркальные", NormalizedName = "reflectors" },
                new() { Name = "Линзовые", NormalizedName = "refractors" },
                new() { Name = "Зеркально-Линзовые", NormalizedName = "catadioptrics" },
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            var telescopes = new List<TelescopeEntity>
            {
                new() { Name = "Sky-Watcher 150/750", Description = "Отличный телескоп для новичков", Category = categories.First(c => c.NormalizedName == "reflectors"), Price = 1000, ImagePath = "https://localhost:7002/Images/SkyWatcher15075.jpg", MimeType = ".jpg" },
                new() { Name = "Celestron AstroMaster 70AZ", Description = "Легкий и портативный рефрактор", Category = categories.First(c => c.NormalizedName == "refractors"), Price = 800, ImagePath = "https://localhost:7002/Images/AstroMaster70AZ.jpeg", MimeType = ".jpeg" },
                new() { Name = "Meade LX90 8-inch", Description = "Катадиоптрический телескоп с отличной оптикой", Category = categories.First(c => c.NormalizedName == "catadioptrics"), Price = 2000, ImagePath = "https://localhost:7002/Images/LX90.jpg", MimeType = ".jpg" },
                new() { Name = "Orion SkyQuest XT8", Description = "Надежный рефлектор для наблюдений за глубоким небом", Category = categories.First(c => c.NormalizedName == "reflectors"), Price = 1200, ImagePath = "https://localhost:7002/Images/SkyQuestXT8.jpg", MimeType = ".jpg" },
                new() { Name = "Celestron NexStar 6SE", Description = "Комбинированный телескоп с автонаведением", Category = categories.First(c => c.NormalizedName == "catadioptrics"), Price = 1500, ImagePath = "https://localhost:7002/Images/NexStar6SE.jpg", MimeType = ".jpg" },
                new() { Name = "William Optics Zenithstar 73", Description = "Премиум рефрактор для астрономов", Category = categories.First(c => c.NormalizedName == "refractors"), Price = 1700, ImagePath = "https://localhost:7002/Images/Zenithstar73.jpg", MimeType = ".jpg" },
            };

            await context.Telescopes.AddRangeAsync(telescopes);
            await context.SaveChangesAsync();
        }
    }
}