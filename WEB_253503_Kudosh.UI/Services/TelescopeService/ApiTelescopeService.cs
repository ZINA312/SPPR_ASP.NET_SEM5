using System.Net.Http;
using System.Text;
using System.Text.Json;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;

namespace WEB_253503_Kudosh.UI.Services.TelescopeProductService
{
    public class ApiTelescopeService : ITelescopeService
    {
        HttpClient _httpClient;
        string _pageSize;
        JsonSerializerOptions _serializerOptions;
        ILogger _logger;
        public ApiTelescopeService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiTelescopeService> logger)
        {
            _httpClient = httpClient;
            _pageSize = configuration.GetSection("ItemsPerPage").Value;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }
        public Task<ResponseData<TelescopeEntity>> CreateProductAsync(TelescopeEntity product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<TelescopeEntity>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<ListModel<TelescopeEntity>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            // подготовка URL запроса
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}telescopes?");
            // добавить категорию в строку запроса
            if (!string.IsNullOrEmpty(categoryNormalizedName))
            {
                urlString.Append($"category={Uri.EscapeDataString(categoryNormalizedName)}&");
            }

            // добавить номер страницы в строку запроса
            if (pageNo > 1)
            {
                urlString.Append($"pageNo={pageNo}&");
            }

            // добавить размер страницы в строку запроса
            if (!_pageSize.Equals("3"))
            {
                urlString.Append($"pageSize={_pageSize}");
            }

            // Удалить последний символ '&', если он существует
            if (urlString[urlString.Length - 1] == '&')
            {
                urlString.Length--; // Удаляем последний символ
            }
            _logger.LogInformation(urlString.ToString());
            // отправить запрос к API
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<ListModel<TelescopeEntity>>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return ResponseData<ListModel<TelescopeEntity>>.Error($"Ошибка: {ex.Message}");
                }
            }
            _logger.LogError($"-----> Данные не получены от сервера. Error:{ response.StatusCode.ToString()}");
            return ResponseData<ListModel<TelescopeEntity>>.Error($"Данные не получены от сервера. Error:{response.StatusCode.ToString()}");
        }


        public Task UpdateProductAsync(int id, TelescopeEntity product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
