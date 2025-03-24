using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.SharedContext.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64(this string value)
            => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }
}
