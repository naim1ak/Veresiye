﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veresiye.Model
{
    public class Company:BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string  Region { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }

        public static Company FirstOrDefault(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
