using System;
using System.Linq;

namespace Promocodoz.Domain.Interfaces
{
    public interface IRepository : IDisposable
    {
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;
        TEntity GetById<TEntity>(object id) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void Save();
    }
}