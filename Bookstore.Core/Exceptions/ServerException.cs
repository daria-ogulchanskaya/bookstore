using System;
using System.Net;

namespace Bookstore.Core.Exceptions
{
    public class ServerException : Exception {
        public HttpStatusCode StatusCode { get; protected set; }

        public ServerException(HttpStatusCode statusCode, string message)
            : base(message) => 
            StatusCode = statusCode;
    }
}
