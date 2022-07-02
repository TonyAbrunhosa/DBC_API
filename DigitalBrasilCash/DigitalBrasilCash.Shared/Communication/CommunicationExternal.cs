using Newtonsoft.Json;
using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DigitalBrasilCash.Shared.Communication
{
    public static class CommunicationExternal
    {
        public async static Task<T> Get<T>(string uri)
        {   
            HttpClientHandler handler = new HttpClientHandler();
            using (var client = new HttpClient(handler))
            {   
                var retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(5, i => TimeSpan.FromSeconds(10));
                using (var response = await retryPolicy.ExecuteAsync(() => client.GetAsync(uri)))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var dados = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<T>(dados);
                    }
                    else
                        return default(T);
                }
            }
        }
    }
}
