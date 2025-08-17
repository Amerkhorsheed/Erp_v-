using System.Threading.Tasks;

namespace Erp.WebApp.Services.Interfaces
{
    public interface IApiService
    {
        Task<T> PostAsync<T>(string endpoint, object data, string token);
        Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest data, string token = null);
        Task<TResponse> GetAsync<TResponse>(string uri, string token);
    }
}