using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Models.Owns
{
    public class Address
    {
        public string? Street { get; set; }
        public string? District { get; set; }
        public string? Complement { get; set; }
        public string? PostalCode { get; set; }
        public int? Number { get; set; }
    }
}
