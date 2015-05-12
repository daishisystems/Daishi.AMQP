#region Includes

using System;
using System.Text;

#endregion

namespace Daishi.AMQP {
    internal static class RabbitMQAuthorizationHeader {
        public static string Create(string userName, string password) {
            var authHeaderValue = Encoding.UTF8.GetBytes(string.Concat(userName, ":", password));
            var base64EncodedAuthHeaderValue = Convert.ToBase64String(authHeaderValue);

            return string.Concat("Authorization: ", "Basic ", base64EncodedAuthHeaderValue);
        }
    }
}