using NZWalks.Web.Models;
using NZWalks.Web.Services.IServices;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace NZWalks.Web.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse ResponeModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.ResponeModel = new();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiRespone = null;

                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }
                apiRespone = await client.SendAsync(message);

                var apiContent = await apiRespone.Content.ReadAsStringAsync();
                try
                {
                    APIResponse ApiRespone = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if (ApiRespone != null && (apiRespone.StatusCode == HttpStatusCode.BadRequest || apiRespone.StatusCode == HttpStatusCode.NotFound))
                    {
                        ApiRespone.StatusCode = HttpStatusCode.BadRequest;
                        ApiRespone.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(ApiRespone);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }


                }
                catch (Exception e)
                {
                    var exceptionRespone = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionRespone;
                }
                var APIRespone = JsonConvert.DeserializeObject<T>(apiContent);
                return APIRespone;

            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIRespone = JsonConvert.DeserializeObject<T>(res);
                return APIRespone;
            }
        }
    }
}
