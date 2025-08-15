using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Erp.WebApp.Services
{
    public interface IApiService
    {
        Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest data, string token = null);
        Task<TResponse> GetAsync<TResponse>(string uri, string token);
    }

    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        // The HttpClient is now injected, which is a best practice.
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest data, string token = null)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, content);

            // This provides more detailed error information.
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }

        public async Task<TResponse> GetAsync<TResponse>(string uri, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(uri);

            // If the response is NOT successful, read the content and throw a detailed exception.
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                // This gives you the full error from the API instead of a generic message.
                throw new HttpRequestException($"Request to {uri} failed with status code {response.StatusCode}. Response: {errorContent}");
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(jsonResult);
        }
    }
}