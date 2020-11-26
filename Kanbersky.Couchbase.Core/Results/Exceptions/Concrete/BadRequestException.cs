using Kanbersky.Couchbase.Core.Results.Exceptions.Abstract;
using Microsoft.AspNetCore.Http;
using System;

namespace Kanbersky.Couchbase.Core.Results.Exceptions.Concrete
{
    public class BadRequestException : Exception, IBaseException
    {
        public int BaseStatusCode { get; set; }

        public BadRequestException()
        {
            BaseStatusCode = StatusCodes.Status400BadRequest;
        }

        public BadRequestException(string message) : base(message)
        {
            BaseStatusCode = StatusCodes.Status400BadRequest;
        }

        public BadRequestException(string message, Exception exception) : base(message, exception)
        {
            BaseStatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
