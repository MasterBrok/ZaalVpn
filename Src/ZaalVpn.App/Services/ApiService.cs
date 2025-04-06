
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows;
using Accessibility;
using Newtonsoft.Json;
using ZaalVpn.ViewModel;

namespace ZaalVpn.App.Services
{
    public interface IApiService
    {
        Task<ResultModel> PostAsync<M>(string endPoint, M model);
        Task<ApiResponse<R>> GetAsync<R, P>(string endPoint, P? paramert) where P : class;
        public string BaseAddress { get; set; }
    }
    public class ApiService : IApiService
    {

        private readonly HttpClient _client;

        private readonly CookieContainer _cookieContainer;

        public ApiService()
        {
            _cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = _cookieContainer,
                UseCookies = true
            };
            _client = new(handler);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));
            //_client.BaseAddress = new Uri("https://localhost:7198",UriKind.Absolute);
        }
        public string BaseAddress { get; set; }

        public async Task<ResultModel> PostAsync<M>(string endPoint, M model)
        {
            var result = new ResultModel();

            try
            {

                string body = JsonConvert.SerializeObject(model);
                
                var message = await _client.PostAsync($".../{endPoint}", new StringContent(body,Encoding.UTF8, "application/json"));

                message.EnsureSuccessStatusCode();

                if (!message.IsSuccessStatusCode)
                    return result.Set(HttpStatusCode.BadRequest).Failed(OperationMessage.Failed,message.ReasonPhrase);


                var content = await message.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultModel>(content);
            }
            catch (Exception e)
            {
                return result.Set(HttpStatusCode.BadRequest).Failed(e.Message);
            }
        }

        public async Task<ApiResponse<R>> GetAsync<R, P>(string endPoint, P paramert) where P : class
        {
            var response = new ApiResponse<R>();
            string paramerters = "";
            if (paramert is not null)
                paramerters = GenerateQueryString(paramert);

            var result = await _client.GetAsync($".../{endPoint}");
            result.EnsureSuccessStatusCode();
            if (!result.IsSuccessStatusCode)
                return response.Set(HttpStatusCode.BadRequest).FailedErrors("Request is failed");

            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<R>>(content);
        }
        private string GenerateQueryString<P>(P parameters)
        {
            var properties = typeof(P).GetProperties();
            var queryString = new List<string>();

            foreach (var property in properties)
            {
                var value = property.GetValue(parameters);
                if (value != null)
                {
                    queryString.Add($"{property.Name}={Uri.EscapeDataString(value.ToString())}");
                }
            }

            return string.Join("&", queryString);
        }
    }
}
