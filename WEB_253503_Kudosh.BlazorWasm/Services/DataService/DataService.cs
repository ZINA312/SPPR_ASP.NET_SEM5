using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using WEB_253503_Kudosh.BlazorWasm.Pages;
using WEB_253503_Kudosh.BlazorWasm.Services.DataService;
using WEB_253503_Kudosh.Domain.Entities;
using WEB_253503_Kudosh.Domain.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

public class DataService : IDataService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;
    private readonly IAccessTokenProvider _tokenProvider;
    private readonly string _pageSize = "3";

    public DataService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;
        _serializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public List<CategoryEntity> Categories { get; set; }
    public List<TelescopeEntity> Telescopes { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public CategoryEntity SelectedCategory { get; set; }

    public event Action DataLoaded;

    public async Task GetCategoryListAsync()
    {
        var tokenRequest = await _tokenProvider.RequestAccessToken();
        if (tokenRequest.TryGetToken(out var token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Value);
        }
        var route = new StringBuilder("telescopecategories/");
        var response = await _httpClient.GetAsync(route.ToString());
        if (response.IsSuccessStatusCode)
        {
            Categories = (await response.Content.ReadFromJsonAsync<ResponseData<List<CategoryEntity>>>(_serializerOptions)).Data;
        }
    }

    public async Task GetProductListAsync(int pageNo = 1)
    {
        var tokenRequest = await _tokenProvider.RequestAccessToken();
        if (tokenRequest.TryGetToken(out var token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Value);
        }
        
        var route = new StringBuilder("telescopes/");
        List<KeyValuePair<string, string>> queryData = new();
        if (SelectedCategory is not null )
        {
            Console.WriteLine(SelectedCategory.Name);
            queryData.Add(KeyValuePair.Create("category", SelectedCategory.NormalizedName));
        }
        if (pageNo > 1)
        {
            queryData.Add(KeyValuePair.Create("pageNo", pageNo.ToString()));
        }
        
        if (queryData.Count > 0)
        {
            var queryString = QueryHelpers.AddQueryString("", queryData.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
            route.Append(queryString);
        }

        var response = await _httpClient.GetAsync(route.ToString());
        if (response.IsSuccessStatusCode)
        {
            var data = (await response.Content.ReadFromJsonAsync<ResponseData<ListModel<TelescopeEntity>>>(_serializerOptions)).Data;
            Telescopes = data.Items;
            TotalPages = data.TotalPages;
            CurrentPage = data.CurrentPage;
            DataLoaded.Invoke();
        }

    }
}