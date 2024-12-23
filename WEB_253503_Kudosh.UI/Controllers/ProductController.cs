﻿using Microsoft.AspNetCore.Mvc;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;
using WEB_253503_Kudosh.UI.Extensions;
using WEB_253503_Kudosh.UI.Services.TelescopeCategoryService;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;


namespace WEB_253503_Kudosh.UI.Controllers
{
    [Route("telescopes")]
    public class ProductController : Controller
    {
        private readonly ITelescopeService _telescopeService;
        private readonly ICategoryService _categoryService;
        public ProductController(ITelescopeService telescopeService, ICategoryService categoryService)
        {
            _telescopeService = telescopeService;
            _categoryService = categoryService;
        }
        [Route("{category?}")]
        public async Task<IActionResult> Index(string category, int pageNo = 1)
        {
            var productResponse = await _telescopeService.GetProductListAsync(category, pageNo);
            if (!productResponse.Successfull)
                return NotFound(productResponse.ErrorMessage);

            var categoriesResponse = await _categoryService.GetCategoryListAsync();
            if (!categoriesResponse.Successfull)
                return NotFound(categoriesResponse.ErrorMessage);
            var currentCategoryName = categoriesResponse.Data.FirstOrDefault(c =>
            c.NormalizedName.Equals(category, StringComparison.OrdinalIgnoreCase))?.Name ?? "Все категории";

            var model = new ListModel<TelescopeEntity>
            {
                Items = productResponse.Data.Items,
                CurrentPage = productResponse.Data.CurrentPage,
                TotalPages = productResponse.Data.TotalPages
            };

            ViewData["Categories"] = categoriesResponse.Data;
            ViewData["CurrentCategory"] = currentCategoryName ?? "Все категории";

            if (Request.IsAjaxRequest())
            {
                return PartialView("_TelescopeList", model);
            }

            return View(model);
        }
    }
}