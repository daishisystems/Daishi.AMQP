#region Includes

using Daishi.Client;

#endregion

namespace Daishi.AMQP.ConsoleApp {
    internal class Program {
        private static void Main(string[] args) {
            var client = new Client.Client();
            client.Push(new Request {Command = "DOUBLE", Payload = 5}, "mathQueue");
            client.Pop("responseQueue", 500);
        }
    }
}