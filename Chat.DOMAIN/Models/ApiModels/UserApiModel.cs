using Chat.Domain.Models.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Models.ApiModels
{
    public class UserApiModel
    {
        public User User { get; set; }

        //anything else
    }

    public class ContainerUserApiModel
    {
        public List<User> User { get; set; }

        //anyhing else

    }
}
