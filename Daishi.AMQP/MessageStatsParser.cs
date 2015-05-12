#region Includes

using Newtonsoft.Json.Linq;

#endregion

namespace Daishi.AMQP {
    internal static class MessageStatsParser {
        public static void Parse(JToken token, out double consumptionRate, out double dispatchRate) {
            var messageStats = token["message_stats"];
            if (messageStats == null) {
                consumptionRate = 0d;
                dispatchRate = 0d;
            }
            else {
                var publishDetails = messageStats["publish_details"];
                consumptionRate = publishDetails != null ? (double?) publishDetails["rate"] ?? 0d : 0d;

                var deliverDetails = messageStats["deliver_details"];
                dispatchRate = deliverDetails != null ? (double?) deliverDetails["rate"] ?? 0d : 0d;
            }
        }
    }
}