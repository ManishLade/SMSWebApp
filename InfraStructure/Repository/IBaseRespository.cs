using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository
{
    public interface IBaseRespository<TEntity> : IDisposable where TEntity : class
    {
        void AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> GetSingleBySpec(ISpecification<TEntity> spec);
        IQueryable<TEntity> GetAll();
        //void Update(TEntity obj);
        void Remove(Guid id);
        int SaveChanges();
        Task SaveChangesAsync();
    }

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
    }
}
