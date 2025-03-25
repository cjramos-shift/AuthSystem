using Auth.Application.DTOs.Requests;

namespace Auth.Application.Ports;

public interface IAuth
{
    public string JwtAuthHandler(UserLoginDTO user);
}
