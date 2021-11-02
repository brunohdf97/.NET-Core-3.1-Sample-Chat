using Chat.Domain.Models.Tables;
using Chat.Domain.Models.ViewModels;
using Chat.Infra.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Services
{
    public class LoginService
    {
        private readonly UserRepository _userRepository = null;

        //if there is no database, use those users as example to connect into chat
        private readonly List<UserViewModel> _userExamples = new List<UserViewModel>()
        {
            new UserViewModel(){
            User = new User()
                {
                    Id = 1,
                    Email = "teste@gmail.com",
                    Name = "TESTE",
                    Password = "e10adc3949ba59abbe56e057f20f883e",///md5 de 123456 (only a simple example)

                },
            }, new UserViewModel(){
                User = new User()
                {
                    Id = 2,
                    Email = "teste2@gmail.com",
                    Name = "TESTE2",
                    Password = "e10adc3949ba59abbe56e057f20f883e",///md5 de 123456 (only a simple example)

                },
                },
             new UserViewModel(){
                User = new User()
                {
                    Id = 3,
                    Email = "teste3@gmail.com",
                    Name = "TESTE3",
                    Password = "e10adc3949ba59abbe56e057f20f883e",///md5 de 123456 (only a simple example)

                },
             }
        };

        public LoginService()
        {
            _userRepository = new UserRepository();
        }

        public UserViewModel GetUser(string email)
        {
            //if you runs migration it will work (mysql running on machine with correct connection string)
            //if (!string.IsNullOrWhiteSpace(email))
            //return _user_repository.GetAsQueryable().Select(a => new UserViewModel() { User = a }).FirstOrDefault(a => a.User.Email == email);

            return _userExamples.FirstOrDefault(a => a.User.Email == email);
        }

        public bool IsUserValid(UserViewModel userVModel)
        {
            //if you runs migration it will work (mysql running on machine with correct connection string)
            //if (user_vmodel != null)
            //{
            //    var user_db = _user_repository.Get(user_vmodel.User.Id);
            //    if (user_db != null)
            //        return true;
            //}

            //otherwise as its only a test
            if (userVModel != null && _userExamples.Any(a => a.User.Email == userVModel.User.Email))
                return true;

            return false;
        }

        public bool IsUserValid(string email, string password)
        {
            //if you runs migration it will work (mysql running on machine with correct connection string)
            //var user_db = _user_repository.GetAsQueryable().FirstOrDefault(a => a.Email == email && a.Password == password);
            //if (user_db != null)
            //    return true;

            //otherwise as its only a test
            if (_userExamples.Any(a => a.User.Email == email && a.User.Password == password))
                return true;

            return false;
        }
    }
}
