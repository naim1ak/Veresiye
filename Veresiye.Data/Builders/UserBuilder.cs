using System.Data.Entity.ModelConfiguration;
using Veresiye.Model;

namespace Veresiye.Data.Builders
{
    public class UserBuilder
    {
        //private EntityTypeConfiguration<User> entityTypeConfiguration;

        public UserBuilder(EntityTypeConfiguration<User> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.UserName).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Password).IsRequired().HasMaxLength(100);
            builder.Property(a => a.CompanyName).HasMaxLength(100);
            builder.Property(a => a.City).HasMaxLength(100);
            builder.Property(a => a.Phone).HasMaxLength(20);
            builder.Property(a => a.Region).HasMaxLength(100);
            //this.entityTypeConfiguration = entityTypeConfiguration;
        }
    }
}