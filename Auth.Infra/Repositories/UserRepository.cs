using Auth.Application.DTOs.Requests;
using Auth.Application.Ports;
using Auth.Domain.AccountContext.Entities;
using Auth.Domain.AccountContext.ValueObjects;

namespace Auth.Infra.Repositories
{
    public class UserRepository : ILogin
    {
        public User Login(UserLoginDTO user)
        {
            var userRepo = new User()
            {
                Name = user.UserName,
                Password = new Password(user.Password),
                Email = new Email("email@teste.com.br"),
                Role = new string[] { "admin" }
            };

            return userRepo;
        }
    }
}
