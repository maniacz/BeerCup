using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerCup.Contracts.Repository
{
    public interface IGenericRepository
    {
        Task<T> GetAsync<T>(string uri, string authToken = "");

        Task<T> PostAsync<T>(string uri, T data, string authToken = "");

        Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest request, string authToken = ""); 
    }
}
