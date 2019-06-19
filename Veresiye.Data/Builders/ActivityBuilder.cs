using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veresiye.Model;

namespace Veresiye.Data.Builders
{
    public class ActivityBuilder
    {
        public ActivityBuilder(EntityTypeConfiguration<Activity> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            
        }

    }
}
