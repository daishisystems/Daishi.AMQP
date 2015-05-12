#region Includes

using System;

#endregion

namespace Daishi.AMQP {
    internal static class ConsumerUtilisationParser {
        public static int Parse(string consumerUtilisation) {
            if (string.IsNullOrEmpty(consumerUtilisation))
                return -1;
            if (consumerUtilisation.Contains("E"))
                return 0;
            var percentageValue = Convert.ToDecimal(consumerUtilisation) * 100;
            return Convert.ToInt32(decimal.Round(percentageValue));
        }
    }
}