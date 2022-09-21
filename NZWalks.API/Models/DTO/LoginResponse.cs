using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class LoginResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
