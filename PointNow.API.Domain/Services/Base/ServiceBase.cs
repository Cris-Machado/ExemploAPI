using PointNow.API.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace PointNow.API.Domain.Interfaces.Base
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        #region ## Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<TEntity> _repository;
        #endregion

        #region ## Ctor | Dispose
        public ServiceBase(IUnitOfWork unitOfWork, IRepositoryBase<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ## Transaction
        public void BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
        }
        public async Task<bool> SaveChangesAsync()
        {
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        #endregion

        #region ## CRUD
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var result = await _repository.InsertAsync(entity);
            await SaveChangesAsync();

            return result;
        }
        public async Task<bool> InsertListAsync(IEnumerable<TEntity> listEntity)
        {
            await _repository.InsertListAsync(listEntity);
            return await SaveChangesAsync();
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = await _repository.UpdateAsync(entity);
            await SaveChangesAsync();

            return result;
        }
        public async Task<bool> DeleteAsync(TEntity entity)
        {
            await _repository.DeleteAsync(entity);
            return await SaveChangesAsync();
        }
        public async Task<bool> DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
            return await SaveChangesAsync();
        }
        #endregion

        #region ## Searches
        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await _repository.FindAllAsync();

        }
        public async Task<TEntity> FindByIdAsync(string id)
        {
            return await _repository.FindByIdAsync(id);
        }
        public async Task<IEnumerable<TEntity>> FindAsync(int skip, int take)
        {
            return await _repository.FindAsync(skip, take);
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }
        public async Task<int> CountAllAsync()
        {
            return await _repository.CountAllAsync();
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.CountAsync(predicate);
        }
        #endregion
    }
}
