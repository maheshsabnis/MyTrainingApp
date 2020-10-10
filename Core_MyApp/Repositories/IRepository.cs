using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_MyApp.Repositories
{
    /// <summary>
    /// An interface that will declare CRUD Methods.
    /// TEntity will be an entity class e.g. Department / Employee
    /// TPk will be the value for Primary Key to Search/Update/Delete record
    /// TEntity is 'constratinted' to aleas class using "where TEntity : class" 
    /// Since ASP.NET Core is more efficient for Async operations, we will make all
    /// methods as Async methods
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPk"></typeparam>
    public interface IRepository<TEntity, in TPk> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(TPk id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TPk id, TEntity entity);
        Task<bool> DeleteAsync(TPk id);
    }
}
