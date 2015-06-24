#region Includes

using System;

#endregion

namespace Daishi.Words {
    public static class Functions {
        public static string Reverse(string input) {
            var array = input.ToCharArray();

            Array.Reverse(array);

            return new string(array);
        }
    }
}