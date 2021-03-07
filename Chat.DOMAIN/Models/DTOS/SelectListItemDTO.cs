using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Models.DTOS
{
    public class SelectListGroupDTO
    {
        public string Name { get; set; }

        public bool Disabled { get; set; }
    }

    public class SelectListItemDTO
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public bool Selected { get; set; }

        public SelectListGroupDTO Group { get; set; }
    }
}
