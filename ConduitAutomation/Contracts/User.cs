using System;
using System.Collections.Generic;
using System.Text;

namespace ConduitAutomation.Contracts
{
    public class User
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Bio { get; set; }

        public string Image { get; set; }

        public string Token { get; set; }
    }

    public class UserResponse
    {
        public User User { get; set; }
    }
}
