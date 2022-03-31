using BeerCup.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Contracts.Repository
{
    public interface IGenericRepository
    {
        Task<ApiResponse<T>> GetAsync<T>(string uri, string authToken = "");

        Task<T> PostAsync<T>(string uri, T data, string authToken = "");

        Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest request, string authToken = "");

        Task<T> DeleteAsync<T>(string uri, string authToken = "");

        Task<T> PutAsync<T>(string uri, T data, string authToken = "");

        Task<TResponse> PutAsync<TRequest, TResponse>(string uri, TRequest data, string authToken = "");
    }
}
