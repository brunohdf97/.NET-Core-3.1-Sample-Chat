using Chat.Domain.Models.Owns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Models.Tables
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Address Address { get; set; }
    }
}
