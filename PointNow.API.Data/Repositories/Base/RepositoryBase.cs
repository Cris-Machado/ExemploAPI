using Microsoft.EntityFrameworkCore;
using PointNow.API.Data.Interfaces;
using PointNow.API.Domain.Interfaces.Repositories;
using System.Linq.Expressions;

namespace PointNow.API.Data.Repositories.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        #region ## Properties
        protected IDbContext Context;
        protected DbSet<TEntity> DbSet;
        #endregion

        #region ## Ctor | Dispose
        public RepositoryBase(IDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        #region ## CRUD
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            return entity;
        }
        public async Task<bool> InsertListAsync(IEnumerable<TEntity> listEntity)
        {
            await Context.Set<TEntity>().AddRangeAsync(listEntity);
            return true;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await Task.Run(() => Context.Set<TEntity>().Update(entity));
            return entity;
        }
        public async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity != null)
            {
                await Task.Run(() => Context.Set<TEntity>().Remove(entity));
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteAsync(string id)
        {
            if (id != null)
            {
                var entity = await Context.Set<TEntity>().FindAsync(id);
                if (entity != null) Context.Set<TEntity>().Remove(entity);
                return true;
            }
            return false;
        }
        #endregion

        #region ## Consultas
        public async Task<IEnumerable<TEntity>> FindAllAsync()
        {
            return await DbSet.ToListAsync();
        }
        public async Task<TEntity> FindByIdAsync(string id)
        {
            return await DbSet.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> FindAsync(int skip, int take)
        {
            return await Context.Set<TEntity>().Skip(skip).Take(take).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }
        public async Task<int> CountAllAsync()
        {
            return await Context.Set<TEntity>().CountAsync();
        }
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().CountAsync(predicate);
        }
        #endregion

    }
}
