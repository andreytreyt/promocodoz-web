using System;
using System.Linq;
using Promocodoz.Domain.Interfaces;

namespace Promocodoz.Infrastructure.Data
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _applicationDbContext = ApplicationDbContext.Create();

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            return _applicationDbContext.Set<TEntity>();
        }

        public TEntity GetById<TEntity>(object id) where TEntity : class
        {
            return _applicationDbContext.Set<TEntity>().Find(id);
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _applicationDbContext.Set<TEntity>().Add(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _applicationDbContext.Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
