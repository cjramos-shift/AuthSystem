using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Auth.Domain.AccountContext.ValueObjects;

namespace Auth.Domain.AccountContext.Entities;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; }

    public Email Email { get; set; }

    public Password Password { get; set; }

    public string[] Role { get; set; }
}
