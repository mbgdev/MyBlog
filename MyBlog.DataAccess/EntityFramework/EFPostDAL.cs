using Microsoft.EntityFrameworkCore;
using MyBlog.DataAccess.Abstract;
using MyBlog.DataAccess.Concrete;
using MyBlog.DataAccess.Repository;
using MyBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccess.EntityFramework
{
    public class EFPostDAL : GenericRepository<Post>, IPostDAL
    { 
        
        public EFPostDAL(Context context) : base(context)
        {
        }

        public List<Post> PostsWithAuthor()
        {

            return _context.Posts.Include(x => x.AppUser).ToList();

            
        }

        public List<Post> PostsWithAuthorId(string id)
        {
            return _context.Posts.Where(x => x.AppUserId == id).Include(x => x.AppUser).ToList();
            ;
        }
    }
}
