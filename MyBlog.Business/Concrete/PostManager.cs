using MyBlog.Business.Abstract;
using MyBlog.DataAccess.Abstract;
using MyBlog.DataAccess.EntityFramework;
using MyBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Concrete
{
    public class PostManager : GenericManager<Post>, IPostService
    {
        private readonly IPostDAL postDAL;

        public PostManager(IGenericRepositoryDAL<Post> repository, IUnitOfWork unitOfWork, IPostDAL postDAL) : base(repository, unitOfWork)
        {
            this.postDAL = postDAL;
        }

        public List<Post> PostsWithAuthor()
        {
            return postDAL.PostsWithAuthor();
        }

        public List<Post> PostsWithAuthor(string id)
        {
            return postDAL.PostsWithAuthorId(id);

        }
    }
}
