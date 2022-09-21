using NZWalks.Web.Models.DTO;

namespace NZWalks.Web.Services.IServices
{
    public interface IWalkDifficultyService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(Guid id, string token);
        Task<T> AddAsync<T>(AddWalkDifficultyRequest region, string token);
        Task<T> DeleteAsync<T>(Guid id, string token);
        Task<T> UpdateAsync<T>(UpdateWalkDifficultyRequest region, string token);
    }
}
