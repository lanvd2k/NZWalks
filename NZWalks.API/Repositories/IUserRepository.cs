using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        //Task<User> AuthenticateAsync(string username, string password);
        bool IsUnique(string username);
        Task<LoginResponse> LoginAsync(LoginRequest loginRequestDTO);
        Task<User> RegisterAsync(RegisterationRequestDTO registerationRequestDTO);
    }
}
