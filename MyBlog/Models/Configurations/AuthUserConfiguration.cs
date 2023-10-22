using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyBlog.Models.Configurations
{
    public class AuthUserConfiguration : IEntityTypeConfiguration<AuthUser>
    {
        public void Configure(EntityTypeBuilder<AuthUser> builder)
        {
            builder
                .Property(x => x.Account)
                .HasColumnType("nvarchar(MAX)")
                .HasColumnName("Account")
                .IsRequired();
            builder
                .Property(x => x.Pwd)
                .HasMaxLength(10)
                .HasColumnType("varchar")
                .HasColumnName("Password")
                .IsRequired();
        }
    }
}
