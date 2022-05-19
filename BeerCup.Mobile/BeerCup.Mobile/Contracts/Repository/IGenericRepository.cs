using BeerCup.Mobile.Models;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Contracts.Repository
{
    public interface IGenericRepository
    {
        Task<ApiResponse<T>> GetAsync<T>(string uri, string authToken = "");

        Task<ApiResponse<T>> PostAsync<T>(string uri, T data, string authToken = "");

        Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string uri, TRequest request, string authToken = "");

        Task<ApiResponse<T>> DeleteAsync<T>(string uri, string authToken = "");

        Task<ApiResponse<T>> PutAsync<T>(string uri, T data, string authToken = "");

        Task<ApiResponse<TResponse>> PutAsync<TRequest, TResponse>(string uri, TRequest data, string authToken = "");
    }
}
