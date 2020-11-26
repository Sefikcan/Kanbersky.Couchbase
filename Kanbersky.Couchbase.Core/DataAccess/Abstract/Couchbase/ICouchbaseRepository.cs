using Kanbersky.Couchbase.Core.Entity;
using Kanbersky.Couchbase.Core.Models;
using System.Threading.Tasks;

namespace Kanbersky.Couchbase.Core.DataAccess.Abstract.Couchbase
{
    public interface ICouchbaseRepository<T> where T : BaseEntity, IEntity
    {
        Task<T> AddAsync(T entity);
        Task Remove(string key);
        Task<T> UpsertAsync(T entity);
        Task<T> FindAsync(string key);
        Task<PageableModel<T>> GetPageable(int pageSize, int page);
    }
}
