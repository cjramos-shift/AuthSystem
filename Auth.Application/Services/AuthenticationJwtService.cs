using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.DTOs.Requests;
using Auth.Application.Ports;
using Auth.Domain.AccountContext.Entities;
using Auth.Domain.AccountContext.ValueObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Application.Services
{
    public class AuthenticationJwtService(IConfiguration _config, ILogin userLogin) : IAuth
    {
        public string JwtAuthHandler(UserLoginDTO user)
        {
            var handler = new JwtSecurityTokenHandler();

            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSecretKey"])), SecurityAlgorithms.HmacSha256);

            var userClaim = userLogin.Login(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddMinutes(20),
                Subject = CreateClaims(userClaim)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        internal static ClaimsIdentity CreateClaims(User user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(type: "id", user.Id.ToString()));
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            ci.AddClaim(new Claim(ClaimTypes.Hash, user.Email.Hash));
            ci.AddClaim(new Claim(ClaimTypes.Email, user.Email.Address));
            ci.AddClaim(new Claim(type: "password", user.Password.PasswordHash));

            foreach (var role in user.Role)
                ci.AddClaim(new Claim(ClaimTypes.Role, role));

            return ci;
        }
    }
}
