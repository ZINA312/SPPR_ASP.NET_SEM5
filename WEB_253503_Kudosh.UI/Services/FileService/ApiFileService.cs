
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using WEB_253503_Kudosh.UI.Services.TelescopeProductService;

namespace WEB_253503_Kudosh.UI.Services.FileService
{
    public class ApiFileService : IFileService
    {

        private readonly HttpClient _httpClient;
        ILogger _logger;
        public ApiFileService(HttpClient httpClient, ILogger<ApiFileService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<string> SaveFileAsync(IFormFile formFile)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post
            };
            var extension = Path.GetExtension(formFile.FileName);
            var newName = Path.ChangeExtension(Path.GetRandomFileName(), extension);
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(formFile.OpenReadStream());
            content.Add(streamContent, "file", newName);
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return String.Empty;
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete
            };
            StringContent content = new StringContent(fileName);
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"-----> Ошибка удаления файла. Error: {response.StatusCode}");
                throw new Exception($"Ошибка удаления файла. Error: {response.StatusCode}");
            }
        }

        
    }
}
