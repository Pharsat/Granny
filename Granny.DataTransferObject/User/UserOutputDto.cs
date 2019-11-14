using System;
using System.Collections.Generic;
using System.Text;

namespace Granny.DataTransferObject.User
{
    public class UserOutputDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
