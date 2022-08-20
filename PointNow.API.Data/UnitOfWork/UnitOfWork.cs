using PointNow.API.Data.Interfaces;
using PointNow.API.Domain.Interfaces.Repositories;

namespace PointNow.API.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region ## Propriedades
        private readonly IDbContext _dbContext;
        private bool _disposed;
        #endregion

        #region ## Construtores e Dispose
        public UnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing) _dbContext.Dispose();
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ## Transação
        public void BeginTransaction()
        {
            _disposed = false;
        }
        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
