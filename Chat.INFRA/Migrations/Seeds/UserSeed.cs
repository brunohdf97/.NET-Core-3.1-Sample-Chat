using Chat.Domain.Models.Tables;


namespace Chat.Infra.Migrations.Seeds
{
    public class UserSeed : ISeed<User>
    {
        public User[] Seed()
        {
            return new User[]
            {
                new User()
                {
                    Id = 1,
                    Email = "teste@gmail.com",
                    Name = "TESTE",
                    Password = "e10adc3949ba59abbe56e057f20f883e",///md5 de 123456 (only simple example)

                },
                new User()
                {
                    Id = 2,
                    Email = "teste2@gmail.com",
                    Name = "TESTE2",
                    Password = "e10adc3949ba59abbe56e057f20f883e",///md5 de 123456 (only simple example)

                },
                new User()
                {
                    Id = 3,
                    Email = "teste3@gmail.com",
                    Name = "TESTE3",
                    Password = "e10adc3949ba59abbe56e057f20f883e",///md5 de 123456 (only simple example)

                },
            };
        }

        public object[] SeedOwnesField_Address()
        {
            return new object[]
            {
                new {
                        UserId = 1,
                        District = "TESTE BAIRRO",
                        PostalCode = "00000-000",
                        Complement = "TESTE",
                        Number = 00,
                        Street = "TESTE RUA"
                },
            };
        }
    }
}
