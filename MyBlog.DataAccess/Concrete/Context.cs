using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccess.Concrete
{
    public class Context: IdentityDbContext<AppUser, AppRole, string>
    {
        public Context(DbContextOptions options) : base(options)
        {
        }


      public  DbSet<Post> Posts { get; set; }
      public  DbSet<Message> Messages { get; set; }

    }
}
