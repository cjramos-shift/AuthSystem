using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.SharedContext.Exceptions
{
    public class PasswordWeakException : Exception
    {
        private const string messageReturn = "A senha não segue os padrões impostos para ser considerada forte!";

        public PasswordWeakException() : base(messageReturn)
        {
        }

        public PasswordWeakException(string? message) : base(messageReturn)
        {
        }

        public PasswordWeakException(string? message, Exception? innerException) : base(messageReturn, innerException)
        {
        }

        protected PasswordWeakException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
