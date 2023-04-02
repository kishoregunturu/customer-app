using CustomerAPI.Core.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CustomerAPI.Security
{
    public  class JWTHelper
    {
        private IConfiguration configuration { get; set; }
        public JWTHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Generate JWT. Ideally we will use SSO or Outh2.0 Identity Provider
        /// or Aspnet membership
        /// </summary>
        /// <returns></returns>
        public LoginResponse GenerateToken()
        {
            try
            {
                var issuer = configuration["Jwt:Issuer"];
                var audience = configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes
                (configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim("Id", Guid.NewGuid().ToString()),

                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                return new LoginResponse
                {
                    UserId = Guid.NewGuid().ToString(),
                    Token = tokenHandler.WriteToken(token)
                };
            }
            catch
            {

                throw;
            }
            
        }
    }
}
