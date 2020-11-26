using Kanbersky.Couchbase.Core.Results.ApiResponses.Abstract;
using Kanbersky.Couchbase.Core.Results.ApiResponses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.Couchbase.Core.Results.ApiResponses.Concrete
{
    public class KanberskyCreatedObjectResult<T> : ObjectResult, IKanberskyActionResult
    {
        public KanberskyCreatedObjectResult(T result) : base(new KanberskyBaseObjectResultModel<T>(result))
        {
            StatusCode = StatusCodes.Status201Created;
        }
    }
}
