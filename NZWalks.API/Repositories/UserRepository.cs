using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        

        private readonly NZWalksDbContext _context;
        private string secretKey;
        public UserRepository(NZWalksDbContext context, IConfiguration configuration)
        {
            _context = context;
            secretKey = configuration.GetValue<string>("JWT:Key");
        }

        public bool IsUnique(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequestDTO)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower()
                && u.Password == loginRequestDTO.Password);
            if (user == null)
            {
                return new LoginResponse()
                {
                    Token = "",
                    User = null
                };
            }

            //if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponse loginResponseDTO = new LoginResponse()
            {
                Token = tokenHandler.WriteToken(token),
                User = user
            };
            return loginResponseDTO;
        }

        public async Task<User> RegisterAsync(RegisterationRequestDTO registerationRequestDTO)
        {
            User user = new User()
            {
                UserName = registerationRequestDTO.UserName,
                Name = registerationRequestDTO.Name,
                Password = registerationRequestDTO.Password,
                Role = registerationRequestDTO.Role
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
