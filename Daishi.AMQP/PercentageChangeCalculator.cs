#region Includes

using System;

#endregion

namespace Daishi.AMQP {
    internal static class PercentageChangeCalculator {
        public static int Calculate(int num1, int num2) {
            if (num1.Equals(0)) return 0;
            var percentageChange = new decimal((num1 - num2) / num1) * 100;
            return Convert.ToInt32(decimal.Round(percentageChange));
        }

        public static int Calculate(double num1, double num2) {
            if (num1.Equals(0)) return 0;
            var percentageChange = new decimal((num1 - num2) / num1) * 100;
            return Convert.ToInt32(decimal.Round(percentageChange));
        }
    }
}