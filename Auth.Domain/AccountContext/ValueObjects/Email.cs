using System.Text.RegularExpressions;
using Auth.Domain.SharedContext.Extensions;
using Auth.Domain.SharedContext.ValueObjects;

namespace Auth.Domain.AccountContext.ValueObjects;

public partial class Email : ValueObject
{
    protected const string EmailPattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
    
    public Email(string address)
    {
        // Chamada do método 1
        //if(!IsValid(address))
        //    throw new InvalidDataException($"E-mail {address} inválido!");

        Address = address.Trim();

        if (string.IsNullOrEmpty(Address))
            throw new ArgumentNullException(nameof(Address), "E-mail não pode ser vazio!");

        if (Address.Length < 5)
            throw new InvalidDataException("E-mail muito curto!");

        if (Address.Length > 200)
            throw new InvalidDataException("E-mail muito grande!");

        // Chamada do método 2
        if (!EmailRegex().IsMatch(Address))
            throw new InvalidDataException($"E-mail {Address} inválido!");
    }

    public string Address { get; }

    public string Hash => Address.ToBase64();

    // Método 1 de validação de e-mail
    public static bool IsValid(string address) => Regex.IsMatch(address, EmailPattern);

    // Método 2 de validação de e-mail
    [GeneratedRegex(EmailPattern)]
    private static partial Regex EmailRegex(); 
}
