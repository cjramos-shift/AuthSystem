using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Auth.Domain.SharedContext.ValueObjects;

namespace Auth.Domain.AccountContext.ValueObjects;

public class Password : ValueObject
{
    public Password(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password), "Senha não pode ser vazia!");
        if (!ValidateCredential(password))
            throw new InvalidDataException("A senha não segue os padrões impostos para ser considerada forte!");

        Guid passwordCript = Guid.NewGuid();

        PasswordHash = password;
    }

    public string PasswordHash { get; }

    public static bool ValidateCredential(string password)
    {
        if (password.Length < 6)
            return false;

        bool hasUpperCase = Regex.IsMatch(password, "[A-Z]");
        bool hasLowerCase = Regex.IsMatch(password, "[a-z]");
        bool hasDigit = Regex.IsMatch(password, "[0-9]");
        bool hasSpecialChar = Regex.IsMatch(password, "[^a-zA-Z0-9]");

        return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
    }
}
