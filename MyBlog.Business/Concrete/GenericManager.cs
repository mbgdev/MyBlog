using MyBlog.Business.Abstract;
using MyBlog.DataAccess.Abstract;
using MyBlog.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyBlog.Business.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        protected readonly IGenericRepositoryDAL<T> _repository;
        protected readonly IUnitOfWork _unitOfWork;

        public GenericManager(IGenericRepositoryDAL<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
