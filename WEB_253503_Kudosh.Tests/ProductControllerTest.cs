using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;
using WEB_253503_Kudosh.UI.Controllers;
using WEB_253503_Kudosh.UI.Services.TelescopeCategoryService;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;

public class ProductControllerTests
{
    private readonly ITelescopeService _telescopeService;
    private readonly ICategoryService _categoryService;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _telescopeService = Substitute.For<ITelescopeService>();
        _categoryService = Substitute.For<ICategoryService>();
        _controller = new ProductController(_telescopeService, _categoryService);
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenCategoryListFails()
    {
        // Arrange
        var productResponse = ResponseData <ListModel<TelescopeEntity>>.Success(new ListModel<TelescopeEntity>());
        productResponse.Successfull = true;

        _telescopeService.GetProductListAsync(Arg.Any<string>(), Arg.Any<int>())
            .Returns(Task.FromResult(productResponse));
        _categoryService.GetCategoryListAsync().Returns(Task.FromResult(ResponseData<List<CategoryEntity>>.Error("Error fetching categories")));

        // Act
        var result = await _controller.Index("some-category");

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Error fetching categories", notFoundResult.Value);
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenProductListFails()
    {
        // Arrange
        _categoryService.GetCategoryListAsync().Returns(Task.FromResult(ResponseData<List<CategoryEntity>>.Success(new List<CategoryEntity>())));

        var productResponse = ResponseData<ListModel<TelescopeEntity>>.Error("Error fetching products");

        _telescopeService.GetProductListAsync(Arg.Any<string>(), Arg.Any<int>())
            .Returns(Task.FromResult(productResponse));

        // Act
        var result = await _controller.Index("some-category");

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Error fetching products", notFoundResult.Value);
    }

    [Fact]
    public async Task Index_ReturnsViewWithValidData_WhenSuccessful()
    {
        // Arrange
        var categories = new List<CategoryEntity>
        {
            new CategoryEntity(1, "Телескопы", "телескопы"),
            new CategoryEntity(2, "Аксессуары", "аксессуары")
        };

        var productList = new List<TelescopeEntity>
        {
            new TelescopeEntity(1, "Телескоп 1", "Описание 1", categories[0], 100.0, "img1.jpg"),
            new TelescopeEntity(2, "Телескоп 2", "Описание 2", categories[0], 150.0, "img2.jpg")
        };

        var productResponse = ResponseData<ListModel<TelescopeEntity>>.Success(new ListModel<TelescopeEntity>
        {
            Items = productList,
            CurrentPage = 1,
            TotalPages = 1
        });

        var categoryResponse = ResponseData<List<CategoryEntity>>.Success(categories);

        var controllerContext = new ControllerContext();
        var moqHttpContext = new Mock<HttpContext>();
        moqHttpContext.Setup(c => c.Request.Headers).Returns(new HeaderDictionary());
        controllerContext.HttpContext = moqHttpContext.Object;
        var controller = new ProductController(_telescopeService, _categoryService) { ControllerContext = controllerContext };

        _telescopeService.GetProductListAsync(Arg.Any<string>(), Arg.Any<int>())
            .Returns(Task.FromResult(productResponse));
        _categoryService.GetCategoryListAsync().Returns(Task.FromResult(categoryResponse));

        // Act
        var result = await controller.Index("телескопы");

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<ListModel<TelescopeEntity>>(viewResult.Model);

        Assert.Equal(2, model.Items.Count);
        Assert.Equal("Телескопы", viewResult.ViewData["CurrentCategory"]);
        Assert.Equal(categories, viewResult.ViewData["Categories"]);
    }
}