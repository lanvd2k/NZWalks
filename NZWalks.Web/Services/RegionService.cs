using Newtonsoft.Json.Linq;
using NZWalks.Web.Models;
using NZWalks.Web.Models.DTO;
using NZWalks.Web.Services.IServices;

namespace NZWalks.Web.Services
{
    public class RegionService : BaseService, IRegionService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string regionUrl;
        public RegionService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            regionUrl = configuration.GetValue<string>("ServiceUrls:NZWalksAPI");
        }

        public Task<T> AddAsync<T>(AddRegionRequest region, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = region,
                Url = regionUrl + "/Regions",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = regionUrl + "/Regions/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = regionUrl + "/Regions",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = regionUrl + "/Regions/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(UpdateRegionRequest region, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = region,
                Url = regionUrl + "/Regions/" + region.Id,
                Token = token
            });
        }
    }
}
