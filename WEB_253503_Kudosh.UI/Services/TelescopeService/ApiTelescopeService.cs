using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;
using System.Diagnostics;
using WEB_253503_Kudosh.UI.Services.FileService;
using WEB_253503_Kudosh.UI.Services.Authentication;

namespace WEB_253503_Kudosh.UI.Services.TelescopeProductService
{
    public class ApiTelescopeService : ITelescopeService
    {
        HttpClient _httpClient;
        IFileService _fileService;
        string _pageSize;
        JsonSerializerOptions _serializerOptions;
        ILogger _logger;
        ITokenAccessor _tokenAccessor;
        public ApiTelescopeService(HttpClient httpClient, IConfiguration configuration, 
            ILogger<ApiTelescopeService> logger, IFileService fileService, ITokenAccessor accessor)
        {
            _httpClient = httpClient;
            _pageSize = configuration.GetSection("ItemsPerPage").Value;
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
            _fileService = fileService;
            _tokenAccessor = accessor;
        }
        public async Task<ResponseData<TelescopeEntity>> CreateProductAsync(TelescopeEntity product, IFormFile? formFile)
        {
            using var content = new MultipartFormDataContent();

            product.ImagePath = "https://localhost:7002/Images/noimage.jpg";
            if (formFile != null)
            {
                var imageUrl = await _fileService.SaveFileAsync(formFile);
                if (!string.IsNullOrEmpty(imageUrl))
                    product.ImagePath = imageUrl;
            }

            var jsonProduct = JsonSerializer.Serialize(product, _serializerOptions);
            content.Add(new StringContent(jsonProduct, Encoding.UTF8, "application/json"), "product");
            _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
            var response = await _httpClient.PostAsync("telescopes", content);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<TelescopeEntity>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return ResponseData<TelescopeEntity>.Error($"Ошибка: {ex.Message}");
                }
            }

            _logger.LogError($"-----> Данные не получены от сервера. Error:{response.StatusCode}");
            return ResponseData<TelescopeEntity>.Error($"Данные не получены от сервера. Error:{response.StatusCode}");
        }

        public async Task DeleteProductAsync(int id)
        {
            _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
            var response = await _httpClient.DeleteAsync($"telescopes/{id}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"-----> Ошибка удаления продукта. Error: {response.StatusCode}");
                throw new Exception($"Ошибка удаления продукта. Error: {response.StatusCode}");
            }
        }

        public async Task<ResponseData<TelescopeEntity>> GetProductByIdAsync(int id)
        {
            _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
            var response = await _httpClient.GetAsync($"telescopes/{id}");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<TelescopeEntity>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return ResponseData<TelescopeEntity>.Error($"Ошибка: {ex.Message}");
                }
            }

            _logger.LogError($"-----> Продукт не найден. Error: {response.StatusCode}");
            return ResponseData<TelescopeEntity>.Error($"Продукт не найден. Error: {response.StatusCode}");
        }

        public async Task<ResponseData<ListModel<TelescopeEntity>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress.AbsoluteUri}telescopes?");
            if (!string.IsNullOrEmpty(categoryNormalizedName))
            {
                urlString.Append($"category={Uri.EscapeDataString(categoryNormalizedName)}&");
            }

            if (pageNo > 1)
            {
                urlString.Append($"pageNo={pageNo}&");
            }

            if (!_pageSize.Equals("3"))
            {
                urlString.Append($"pageSize={_pageSize}");
            }

            if (urlString[urlString.Length - 1] == '&')
            {
                urlString.Length--; 
            }
            _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
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


        public async Task<ResponseData<TelescopeEntity>> UpdateProductAsync(int id, TelescopeEntity product, IFormFile? formFile)
        {
            using var content = new MultipartFormDataContent();
            
            if (formFile != null)
            {
                var getResponse = await _httpClient.GetAsync($"telescopes/{id}");
                if (getResponse.IsSuccessStatusCode)
                {
                    var oldTelescope = getResponse.Content.ReadFromJsonAsync<ResponseData<TelescopeEntity>>(_serializerOptions);
                    await _fileService.DeleteFileAsync(oldTelescope.Result.Data.ImagePath);
                    var imageUrl = await _fileService.SaveFileAsync(formFile);
                    if (!string.IsNullOrEmpty(imageUrl))
                        product.ImagePath = imageUrl;
                }
                else
                {
                    _logger.LogError($"-----> Ошибка обновления продукта. Error: {getResponse.StatusCode}");
                    throw new Exception($"Ошибка обновления продукта. Error: {getResponse.StatusCode}");
                }
            }

            var jsonProduct = JsonSerializer.Serialize(product, _serializerOptions);
            content.Add(new StringContent(jsonProduct, Encoding.UTF8, "application/json"), "product");

            _tokenAccessor.SetAuthorizationHeaderAsync(_httpClient);
            var response = await _httpClient.PutAsync($"telescopes/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<TelescopeEntity>>(_serializerOptions);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    throw new Exception($"Ошибка десериализации: {ex.Message}");
                }
            }

            _logger.LogError($"-----> Ошибка обновления продукта. Error: {response.StatusCode}");
            throw new Exception($"Ошибка обновления продукта. Error: {response.StatusCode}");
        }
    }
}
