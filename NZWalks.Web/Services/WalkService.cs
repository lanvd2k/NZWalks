using Newtonsoft.Json.Linq;
using NZWalks.Web.Models;
using NZWalks.Web.Models.DTO;
using NZWalks.Web.Services.IServices;

namespace NZWalks.Web.Services
{
    public class WalkService : BaseService, IWalkService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string regionUrl;
        public WalkService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            regionUrl = configuration.GetValue<string>("ServiceUrls:NZWalksAPI");
        }

        public Task<T> AddAsync<T>(AddWalkRequest walk, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = walk,
                Url = regionUrl + "/Walks",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = regionUrl + "/Walks/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = regionUrl + "/Walks",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = regionUrl + "/Walks/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(UpdateWalkRequest walk, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = walk,
                Url = regionUrl + "/Walks/" + walk.Id,
                Token = token
            });
        }
    }
}
