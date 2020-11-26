using Kanbersky.Couchbase.Core.Results.ApiResponses.Abstract;
using Kanbersky.Couchbase.Core.Results.ApiResponses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.Couchbase.Core.Results.ApiResponses.Concrete
{
    public class KanberskyUpdatedObjectResult<T> : ObjectResult, IKanberskyActionResult
    {
        public KanberskyUpdatedObjectResult(T result) : base(new KanberskyBaseObjectResultModel<T>(result))
        {
        }
    }
}
