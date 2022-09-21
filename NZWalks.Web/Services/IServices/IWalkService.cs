using NZWalks.Web.Models.DTO;

namespace NZWalks.Web.Services.IServices
{
    public interface IWalkService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(Guid id, string token);
        Task<T> AddAsync<T>(AddWalkRequest walk, string token);
        Task<T> DeleteAsync<T>(Guid id, string token);
        Task<T> UpdateAsync<T>(UpdateWalkRequest walk, string token);
    }
}
