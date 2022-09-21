using NZWalks.Web.Models;

namespace NZWalks.Web.Services.IServices
{
    public interface IBaseService
    {
        APIResponse ResponeModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
