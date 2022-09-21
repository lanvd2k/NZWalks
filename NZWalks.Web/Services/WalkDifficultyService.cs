using Newtonsoft.Json.Linq;
using NZWalks.Web.Models;
using NZWalks.Web.Models.DTO;
using NZWalks.Web.Services.IServices;

namespace NZWalks.Web.Services
{
    public class WalkDifficultyService : BaseService, IWalkDifficultyService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string regionUrl;
        public WalkDifficultyService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            regionUrl = configuration.GetValue<string>("ServiceUrls:NZWalksAPI");
        }

        public Task<T> AddAsync<T>(AddWalkDifficultyRequest addWalkDifficultyRequest, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = addWalkDifficultyRequest,
                Url = regionUrl + "/WalkDifficulty",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = regionUrl + "/WalkDifficulty/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = regionUrl + "/WalkDifficulty",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = regionUrl + "/WalkDifficulty/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(UpdateWalkDifficultyRequest updateWalkDifficultyRequest, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = updateWalkDifficultyRequest,
                Url = regionUrl + "/WalkDifficulty/" + updateWalkDifficultyRequest.Id,
                Token = token
            });
        }
    }
}
