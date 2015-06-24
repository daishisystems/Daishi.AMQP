#region Includes

using Daishi.Words;
using NUnit.Framework;

#endregion

namespace Daishi.UnitTests {
    [TestFixture]
    internal class WordsTests {
        [Test]
        public void WordIsReversed() {
            const string input = "test";
            var output = Functions.Reverse(input);

            Assert.AreEqual("tset", output);
        }
    }
}