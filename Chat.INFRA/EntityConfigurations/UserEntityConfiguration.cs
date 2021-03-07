using Chat.Domain.Models.Tables;
using Chat.Infra.Migrations.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> configuration)
        {
            configuration.HasKey(a => a.Id);
            configuration.Property(a => a.Name).HasColumnType("varchar(255)").IsRequired();
            configuration.Property(a => a.Email).HasColumnType("varchar(255)").IsRequired();
            configuration.Property(a => a.Password).HasColumnType("varchar(255)").IsRequired();

            UserSeed seed = new UserSeed();
            configuration.HasData(seed.Seed());

            configuration.OwnsOne(a => a.Address).HasData(seed.SeedOwnesField_Address());

            configuration.ToTable("users");
        }
    }
}
