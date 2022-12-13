using MyBlog.Business.Abstract;
using MyBlog.DataAccess.Abstract;
using MyBlog.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Concrete
{
    public class MessageManager : GenericManager<Message>, IMessageService
    {
        public MessageManager(IGenericRepositoryDAL<Message> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
