using System;
using System.Collections.Generic;
using System.Text;

using eduLib.Core.Enums;

namespace eduLib.Core.Entities
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role UserRole { get; set; }
    }
}
