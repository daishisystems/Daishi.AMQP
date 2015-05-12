#region Includes

using System.IO;
using Newtonsoft.Json;

#endregion

namespace Daishi.AMQP {
    internal static class ScaleMessage {
        public static string Create(ScaleDirective scaleDirective) {
            var sw = new StringWriter();
            var writer = new JsonTextWriter(sw);
            writer.WriteStartObject();

            writer.WritePropertyName("directive");

            switch (scaleDirective) {
                case ScaleDirective.In:
                    writer.WriteValue("scale-in");
                    break;
                default:
                    writer.WriteValue("scale-out");
                    break;
            }

            writer.WriteEndObject();
            return sw.ToString();
        }
    }
}