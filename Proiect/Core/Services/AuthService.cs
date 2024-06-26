using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICoreDbContext _context;
        private readonly string _securityKey;

        public AuthService(ICoreDbContext context, IConfiguration config)
        {
            _context = context;
            _securityKey = config["Jwt:Key"];
        }

        public async Task Register(string username, string password)
        {
            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public string GetRole(User user)
        {
            return user.Username == "Admin" ? "admin" : "user";
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            return GetToken(user, GetRole(user));
        }

        //public string GetToken(User user, string role)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey)); // ensure key is at least 16 bytes
        //    var roleClaim = new Claim("role", role);
        //    var idClaim = new Claim("userId", user.Id.ToString());
        //    var infoClaim = new Claim("username", user.Username);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Issuer = "Backend",
        //        Audience = "Frontend",
        //        Subject = new ClaimsIdentity(new[] { roleClaim, idClaim, infoClaim }),
        //        Expires = DateTime.Now.AddMinutes(5),
        //        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256) // the token will be signed with this hashing algorithm
        //    };
        //    SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
        //    string token = tokenHandler.WriteToken(securityToken);
        //    return token;
        //}
        public string GetToken(User user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey));
            var roleClaim = new Claim(ClaimTypes.Role, role);
            var idClaim = new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());
            var infoClaim = new Claim(ClaimTypes.Name, user.Username);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "Backend",
                Audience = "Frontend",
                Subject = new ClaimsIdentity(new[] { roleClaim, idClaim, infoClaim }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
