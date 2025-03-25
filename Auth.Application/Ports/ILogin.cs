using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Application.DTOs.Requests;
using Auth.Domain.AccountContext.Entities;

namespace Auth.Application.Ports
{
    public interface ILogin
    {
        public User Login(UserLoginDTO user);
    }
}
