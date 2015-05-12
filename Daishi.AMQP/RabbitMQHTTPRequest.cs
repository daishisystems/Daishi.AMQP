#region Includes

using System;
using System.Net;

#endregion

namespace Daishi.AMQP {
    internal static class RabbitMQHTTPRequest {
        public static WebRequest Create(string prefix, string hostName, int port, string endPoint, string userName, string password) {
            var webRequest = WebRequest.Create(new Uri(string.Concat(prefix, "://", hostName, ":", port, "/", endPoint)));
            webRequest.Headers.Add(RabbitMQAuthorizationHeader.Create(userName, password));

            return webRequest;
        }
    }
}