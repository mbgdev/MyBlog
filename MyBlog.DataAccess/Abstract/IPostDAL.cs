
using MyBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccess.Abstract
{
    public interface IPostDAL:IGenericRepositoryDAL<Post>
    {
        List<Post> PostsWithAuthor();
        List<Post> PostsWithAuthorId(string id);
    }
}
