using Api.Models;
using Api.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Api.data
{
    public interface IJokesContext
    {
        Task<List<string>> GetCategories();
        Task<Search> GetJokes(string query);
    }

    public class JokesContext : IJokesContext
    {
        private const string CategoriesUrl = @"jokes/categories";
        private const string SearchUrl = @"jokes/search";
        private readonly ICacheFactory Cache;
        private readonly ILogger<JokesContext> Logger;
        private readonly RapidApi RapidApiSettings;

        public JokesContext(IOptions<RapidApi> rapidOptions, ILogger<JokesContext> logger, ICacheFactory cache)
        {
            Cache = cache;
            Logger = logger;
            RapidApiSettings = rapidOptions.Value;
        }

        public Task<List<string>> GetCategories() => Cache.Get(CategoriesUrl, () => GetCategoriesFromApi());

        public Task<Search> GetJokes(string query)
        {
            var cacheKey = $"{SearchUrl}?query={query}";
            return Cache.Get(cacheKey, () => GetSearchItemsFromApi($"query={query}"));
        }

        private Task<List<string>> GetCategoriesFromApi() => BuildApiRequest<List<string>>(CategoriesUrl);

        private Task<Search> GetSearchItemsFromApi(string query) => BuildApiRequest<Search>($"{SearchUrl}?{query}");

        private async Task<T> BuildApiRequest<T>(string url)
        {
            var client = new RestClient($"{RapidApiSettings.BaseUrl}/{url}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", RapidApiSettings.Host);
            request.AddHeader("x-rapidapi-key", RapidApiSettings.Key);
            request.AddHeader("accept", "application/json");
            var response = await client.ExecuteGetAsync<T>(request);
            return response.Data;
        }
    }
}