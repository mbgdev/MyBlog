using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccess.Abstract
{
    public interface IGenericRepositoryDAL<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Remove(T entity);
        void Update(T entity);


    }
}
