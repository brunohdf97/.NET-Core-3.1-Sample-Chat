using Chat.Domain.Models.Tables;
using Chat.Infra.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Chat.INFRA.Context
{
    public class MainContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MainContext()
        {
            Database.SetCommandTimeout(0);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //only example
            optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=mydatabase;Uid=myuser;Pwd=mypassword;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            modelBuider.ApplyConfiguration(new UserEntityConfiguration());

            base.OnModelCreating(modelBuider);
        }

    }
}
