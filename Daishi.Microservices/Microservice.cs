#region Includes

using Daishi.AMQP;

#endregion

namespace Daishi.Microservices {
    internal interface Microservice {
        void Init();
        void OnMessageReceived(object sender, MessageReceivedEventArgs e);
        void Shutdown();
    }
}