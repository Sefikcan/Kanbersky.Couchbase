using Kanbersky.Couchbase.Core.Results.ApiResponses.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kanbersky.Couchbase.Core.Results.ApiResponses.Concrete
{
    public class KanberskyOkResult : StatusCodeResult, IKanberskyActionResult
    {
        public KanberskyOkResult() : base(StatusCodes.Status200OK)
        {
        }
    }
}
