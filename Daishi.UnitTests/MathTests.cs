#region Includes

using Daishi.Math;
using NUnit.Framework;

#endregion

namespace Daishi.UnitTests {
    [TestFixture]
    internal class MathTests {
        [Test]
        public void InputIsDoubled() {
            const int input = 5;
            var output = Functions.Double(input);

            Assert.AreEqual(10, output);
        }
    }
}