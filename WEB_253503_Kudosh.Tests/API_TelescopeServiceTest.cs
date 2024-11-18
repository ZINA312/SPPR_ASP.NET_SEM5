using Microsoft.EntityFrameworkCore;
using WEB_253503_Kudosh.API.Data;
using WEB_253503_Kudosh.API.Services.TelescopeService;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;

public class TelescopeServiceTests
{
    private readonly AppDbContext _context;
    private readonly TelescopeService _service;

    public TelescopeServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new AppDbContext(options);
        _service = new TelescopeService(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var categories = new List<CategoryEntity>
        {
            new CategoryEntity { Name = "Category1", NormalizedName = "CAT1"},
            new CategoryEntity { Name = "Category2", NormalizedName = "CAT2"},
        };

        _context.Categories.AddRange(categories);
        _context.SaveChanges();

        var telescopes = new List<TelescopeEntity>
        {
            new TelescopeEntity { Name = "Telescope 1", Category = categories[0], Description = "", ImagePath = "", MimeType = "" },
            new TelescopeEntity { Name = "Telescope 2", Category = categories[1], Description = "", ImagePath = "", MimeType = "" },
            new TelescopeEntity { Name = "Telescope 3", Category = categories[0], Description = "", ImagePath = "", MimeType = ""},
            new TelescopeEntity { Name = "Telescope 4", Category = categories[1], Description = "", ImagePath = "", MimeType = ""},
            new TelescopeEntity { Name = "Telescope 5", Category = categories[0], Description = "", ImagePath = "", MimeType = ""}
        };

        _context.Telescopes.AddRange(telescopes);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetProductListAsync_ReturnsFirstPageWithThreeItems_WhenCalledWithDefaultParameters()
    {
        // Act
        var result = await _service.GetProductListAsync(null);

        // Assert
        var response = Assert.IsType<ResponseData<ListModel<TelescopeEntity>>>(result);
        Assert.True(response.Successfull);
        Assert.Equal(3, response.Data.Items.Count);
        Assert.Equal(2, response.Data.TotalPages);
        Assert.Equal(1, response.Data.CurrentPage);
        _context.Database.EnsureDeleted();

    }

    [Fact]
    public async Task GetProductListAsync_ReturnsCorrectPage_WhenPageNumberIsSpecified()
    {
        // Act
        var result = await _service.GetProductListAsync(null, pageNo: 2);

        // Assert
        var response = Assert.IsType<ResponseData<ListModel<TelescopeEntity>>>(result);
        Assert.True(response.Successfull);
        Assert.Equal(2, response.Data.Items.Count);
        Assert.Equal(2, response.Data.TotalPages);
        Assert.Equal(2, response.Data.CurrentPage);
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task GetProductListAsync_FiltersByCategory_WhenCategoryIsProvided()
    {
        // Act
        var result = await _service.GetProductListAsync("CAT1");

        // Assert
        var response = Assert.IsType<ResponseData<ListModel<TelescopeEntity>>>(result);
        Assert.True(response.Successfull);
        Assert.Equal(3, response.Data.Items.Count);
        Assert.All(response.Data.Items, item => Assert.Equal("CAT1", item.Category.NormalizedName));
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task GetProductListAsync_DoesNotAllowPageSizeGreaterThanMax()
    {
        // Act
        var result = await _service.GetProductListAsync(null, pageSize: 25);

        // Assert
        var response = Assert.IsType<ResponseData<ListModel<TelescopeEntity>>>(result);
        Assert.True(response.Successfull);
        Assert.Equal(5, response.Data.Items.Count); 
        Assert.Equal(1, response.Data.TotalPages);
        _context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task GetProductListAsync_ReturnsError_WhenPageNumberExceedsTotalPages()
    {
        // Act
        var result = await _service.GetProductListAsync(null, pageNo: 3);

        // Assert
        var response = Assert.IsType<ResponseData<ListModel<TelescopeEntity>>>(result);
        Assert.False(response.Successfull);
        Assert.Equal("No such page", response.ErrorMessage);
        _context.Database.EnsureDeleted();
    }
}