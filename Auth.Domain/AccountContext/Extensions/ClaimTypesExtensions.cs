using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.AccountContext.Extensions
{
    public static class ClaimTypesExtensions
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.FindFirst("id")?.Value;

        public static string GetName(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;

        public static string GetPassword(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.FindFirst("password")?.Value;

        public static string[] GetRoles(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();
    }
}
