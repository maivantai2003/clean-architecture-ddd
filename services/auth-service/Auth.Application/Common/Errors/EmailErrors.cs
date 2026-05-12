using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Common.Errors
{
    public static class EmailErrors
    {
        public static readonly Error InvalidEmails =
            new(
                "Auth.InvalidEmail",
                "Invalid email.");
        public static readonly Error EmailAlreadyInUse =
            new(
                "Auth.EmailAlreadyInUse",
                "Email is already in use.");
    }
}
