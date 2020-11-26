using Kanbersky.Couchbase.Core.Results.ApiResponses.Abstract;
using Kanbersky.Couchbase.Core.Results.ApiResponses.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.Couchbase.Core.Results.ApiResponses.Concrete
{
    public class KanberskyOkObjectResult<T> : ObjectResult, IKanberskyActionResult
    {
        public KanberskyOkObjectResult(T result) : base(new KanberskyBaseObjectResultModel<T>(result))
        {
            StatusCode = StatusCodes.Status200OK;
        }
    }
}
