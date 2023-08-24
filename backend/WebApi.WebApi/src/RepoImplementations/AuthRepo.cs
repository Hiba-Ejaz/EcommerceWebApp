
using WebApi.Domain.src.Abstractions;
using WebApi.Domain.src.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.WebApi.src.RepoImplementations
{
    public class AuthRepo : IAuthRepo
    {
        private readonly IConfiguration _configuration;
        public AuthRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var jwtKey = _configuration["Security:JwtKey"];
            jwtKey = string.IsNullOrEmpty(jwtKey) ? "default-key" : jwtKey;
            var jwtIssuer = _configuration["Security:Issuer"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtIssuer,
                Expires = DateTime.Now.AddDays(1),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}