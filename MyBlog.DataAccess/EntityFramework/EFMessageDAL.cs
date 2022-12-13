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
    public class EFMessageDAL : GenericRepository<Message>, IMessageDAL
    {
        public EFMessageDAL(Context context) : base(context)
        {
        }
    }
}
