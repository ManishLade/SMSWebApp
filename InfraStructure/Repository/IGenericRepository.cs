using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InfraStructure.Repository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> Get(
             Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             string includeProperties = "");

        Task<T> GetByID(long id);

        void Insert(T entity);

        Task Delete(long id);

        void Delete(T entityToDelete);

        void Update(T entityToUpdate);

        void Save(T entity);
    }
}