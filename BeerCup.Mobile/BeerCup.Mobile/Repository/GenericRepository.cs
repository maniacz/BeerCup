using BeerCup.Mobile.Contracts.Repository;
using BeerCup.Mobile.Exceptions;
using BeerCup.Mobile.Models;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BeerCup.Mobile.Repository
{
    public class GenericRepository : IGenericRepository
    {
        public async Task<T> GetAsync<T>(string uri, string authToken = "")
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

                //todo: Unathorize'y

                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    //todo: wykorzystać serwis, żeby zlikwodować coupling do view
                    await Application.Current.MainPage.DisplayAlert("Alert", "Serwis niedostępny", "OK");
                    //todo: navigate back lub to root i return
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json = JsonConvert.DeserializeObject<T>(jsonResult);
                return json;


            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name} : {e.Message}");
                throw;
            }
        }

        public async Task<T> PostAsync<T>(string uri, T data, string authToken = "")
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
                    //todo: wykorzystać serwis, żeby zlikwodować coupling do view
                    await Application.Current.MainPage.DisplayAlert("Alert", "Serwis niedostępny", "OK");
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json = JsonConvert.DeserializeObject<T>(jsonResult);
                return json;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name} : {e.Message}");
                throw;
            }
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest request, string authToken = "")
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
                    //todo: wykorzystać serwis, żeby zlikwodować coupling do view
                    await Application.Current.MainPage.DisplayAlert("Alert", "Serwis niedostępny", "OK");
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json = JsonConvert.DeserializeObject<TResponse>(jsonResult);
                return json;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name} : {e.Message}");
                throw;
            }
        }

        public async Task<T> DeleteAsync<T>(string uri, string authToken = "")
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
                    //todo: wykorzystać serwis, żeby zlikwodować coupling do view
                    await Application.Current.MainPage.DisplayAlert("Alert", "Serwis niedostępny", "OK");
                }

                //if (!responseMessage.IsSuccessStatusCode)
                //{
                //    return default(T);
                //}

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json = JsonConvert.DeserializeObject<T>(jsonResult);
                return json;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name} : {e.Message}");
                throw;
            }
        }

        public async Task<T> PutAsync<T>(string uri, T data, string authToken = "")
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
                    //todo: wykorzystać serwis, żeby zlikwodować coupling do view
                    await Application.Current.MainPage.DisplayAlert("Alert", "Serwis niedostępny", "OK");
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json = JsonConvert.DeserializeObject<T>(jsonResult);
                return json;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name} : {e.Message}");
                throw;
            }
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string uri, TRequest data, string authToken = "")
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
                    //todo: wykorzystać serwis, żeby zlikwodować coupling do view
                    await Application.Current.MainPage.DisplayAlert("Alert", "Serwis niedostępny", "OK");
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json = JsonConvert.DeserializeObject<TResponse>(jsonResult);
                return json;
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
