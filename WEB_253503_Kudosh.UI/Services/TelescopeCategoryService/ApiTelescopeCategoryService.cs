using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;

namespace WEB_253503_Kudosh.UI.Services.TelescopeCategoryService
{
    public class ApiTelescopeCategoryService : ICategoryService
    {
        HttpClient _httpClient;
        JsonSerializerOptions _serializerOptions;
        ILogger _logger;
        public ApiTelescopeCategoryService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiTelescopeService> logger)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }
        public async Task<ResponseData<List<CategoryEntity>>> GetCategoryListAsync()
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}telescopecategories/");
            
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<List<CategoryEntity>>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return ResponseData<List<CategoryEntity>>.Error($"Ошибка: {ex.Message}");
                }
            }
            _logger.LogError($"-----> Данные не получены от сервера. Error:{response.StatusCode.ToString()}");
            return ResponseData<List<CategoryEntity>>.Error($"Данные не получены от сервера. Error:{response.StatusCode.ToString()}");
        }
    }
}
