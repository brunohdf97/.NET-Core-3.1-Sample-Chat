using Chat.Domain.Models.DTOS;
using Chat.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Services.Interfaces
{
    public interface ILoginService
    {
        UserViewModel GetUser(string email);
        bool IsUserValid(UsuarioLogadoDTO userVModel);
        bool IsUserValid(string email, string password);
    }
}
