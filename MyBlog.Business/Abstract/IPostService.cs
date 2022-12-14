using MyBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Abstract
{
    public interface IPostService:IGenericService<Post>
    {
       List<Post> PostsWithAuthor();
       List<Post> PostsWithAuthor(string id);
    }
}
