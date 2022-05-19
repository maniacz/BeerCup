using BeerCup.Mobile.Constants;
using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Models;
using Newtonsoft.Json;
using Polly;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BeerCup.Mobile.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly IDialogService _dialogService;

        public GenericRepository(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string uri, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);
                string jsonResult = string.Empty;

                var responseMessage = Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name} : {ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync(
                        1,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(() => httpClient.GetAsync(uri))
                    .GetAwaiter().GetResult();

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden || responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new ApiResponse<T>
                    {
                        Error = ApiErrorResponseConstants.Unauthorized
                    };
                }

                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    // await _dialogService.ShowDialog("Serwis niedostępny", "Alert", "OK");
                    return new ApiResponse<T>
                    {
                        Error = ApiErrorResponseConstants.ServiceUnavailable
                    };
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<ApiResponse<T>>(jsonResult);
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name} : {e.Message}");
                throw;
            }
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name} : {ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync(
                        3,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(uri, content));

                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    // await _dialogService.ShowDialog("Serwis niedostępny", "Alert", "OK");
                    return new ApiResponse<T>
                    {
                        Error = ApiErrorResponseConstants.ServiceUnavailable
                    };
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<ApiResponse<T>>(jsonResult);
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name} : {e.Message}");
                throw;
            }
        }

        public async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string uri, TRequest request, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(request));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"############################## {ex.GetType().Name} : {ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync(
                        1,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(uri, content));

                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    // await _dialogService.ShowDialog("Serwis niedostępny", "Alert", "OK");
                    return new ApiResponse<TResponse>
                    {
                        Error = ApiErrorResponseConstants.ServiceUnavailable
                    };
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<ApiResponse<TResponse>>(jsonResult);
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name} : {e.Message}");
                throw;
            }
        }

        public async Task<ApiResponse<T>> DeleteAsync<T>(string uri, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                string jsonResult = string.Empty;

                var responseMessage = Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name} : {ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync(
                        1,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(() => httpClient.DeleteAsync(uri))
                    .GetAwaiter().GetResult();

                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    await _dialogService.ShowDialog("Serwis niedostępny", "Alert", "OK");
                }

                //if (!responseMessage.IsSuccessStatusCode)
                //{
                //    return default(T);
                //}

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<ApiResponse<T>>(jsonResult);
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name} : {e.Message}");
                throw;
            }
        }

        public async Task<ApiResponse<T>> PutAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name} : {ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync(
                        3,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content));

                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    await _dialogService.ShowDialog("Serwis niedostępny", "Alert", "OK");
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<ApiResponse<T>>(jsonResult);
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name} : {e.Message}");
                throw;
            }
        }

        public async Task<ApiResponse<TResponse>> PutAsync<TRequest, TResponse>(string uri, TRequest data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name} : {ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync(
                        3,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content));

                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    await _dialogService.ShowDialog("Serwis niedostępny", "Alert", "OK");
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<ApiResponse<TResponse>>(jsonResult);
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name} : {e.Message}");
                throw;
            }
        }

        private HttpClient CreateHttpClient(string authToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
            }

            return httpClient;
        }
    }
}
