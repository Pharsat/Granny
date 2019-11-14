using System;
using System.Collections.Generic;
using System.Text;

namespace Granny.DataModel
{
    public class AuthenticatedUser : User
    {
        public string Token { get; set; }
    }
}
