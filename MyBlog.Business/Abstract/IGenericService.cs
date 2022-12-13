using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Abstract
{
    public interface IGenericService<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
