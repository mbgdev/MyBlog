﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entity.Concrete
{
    public class AppUser:IdentityUser
    {
        public ICollection<Post> Posts { get; set; }

        public DateTime  BirthDay { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ImageUrl { get; set; }

        public string City { get; set; }
    }
}
