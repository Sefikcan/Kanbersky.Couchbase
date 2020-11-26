using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Kanbersky.Couchbase.Core.DataAccess.Abstract.Couchbase;
using Kanbersky.Couchbase.Core.Entity;
using Kanbersky.Couchbase.Core.Extensions;
using Kanbersky.Couchbase.Core.Models;
using Kanbersky.Couchbase.Core.Results.Exceptions.Concrete;
using System;
using System.Threading.Tasks;

namespace Kanbersky.Couchbase.Infrastructure.Concrete.Couchbase
{
    public class CouchbaseRepository<T> : ICouchbaseRepository<T> where T : BaseEntity, IEntity
    {
        #region fields

        private readonly IBucketProvider _bucketProvider;
        private readonly IBucket _bucket;

        #endregion

        #region ctor

        public CouchbaseRepository(IBucketProvider bucketProvider)
        {
            _bucketProvider = bucketProvider;
            _bucket = _bucketProvider.GetBucket(typeof(T).Name);
        }

        #endregion

        #region methods

        private async Task<string> PrepareIncrementId()
        {
            var response = await _bucket.IncrementAsync(typeof(T).Name, 1);
            return response.Value.ToString();
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.Id = await PrepareIncrementId();
            var results = await _bucket.InsertAsync(entity.ConvertDocument());
            return results.Document.Content;
        }

        public async Task<T> FindAsync(string key)
        {
            var result = await _bucket.GetDocumentAsync<T>(key);
            return result.Document.ConvertEntity();
        }

        public async Task Remove(string key)
        {
            await _bucket.RemoveAsync(key);
        }

        public async Task<T> UpsertAsync(T entity)
        {
            var result = await _bucket.UpsertAsync<T>(entity.ConvertDocument());
            return result.Document.ConvertEntity();
        }

        public async Task<PageableModel<T>> GetPageable(int pageSize, int page)
        {
            //is not missing ile counter sayacımızı disabled ediyoruz
            var results = await _bucket.QueryAsync<T>($" select Customer.* from Customer where id is not missing order by id OFFSET { (page -1) * pageSize} LIMIT {pageSize} ");
            if (results.Success)
            {
                return new PageableModel<T>
                {
                    Items = results.Rows,
                    PageNumber = page,
                    PageSize = results.Rows.Count,
                    TotalItemCount = Convert.ToInt32(results.Metrics.SortCount),
                    TotalPageCount = (int)Math.Ceiling(Convert.ToDouble(results.Metrics.SortCount) / pageSize)
                };
            }

            throw BaseException.BadRequestException(results.Message);
        }

        #endregion
    }
}
