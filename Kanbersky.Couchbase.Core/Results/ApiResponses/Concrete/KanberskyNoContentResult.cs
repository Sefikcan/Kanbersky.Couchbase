using Kanbersky.Couchbase.Core.Results.ApiResponses.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.Couchbase.Core.Results.ApiResponses.Concrete
{
    public class KanberskyNoContentResult : StatusCodeResult, IKanberskyActionResult
    {
        public KanberskyNoContentResult() : base(StatusCodes.Status204NoContent)
        {
        }
    }
}
