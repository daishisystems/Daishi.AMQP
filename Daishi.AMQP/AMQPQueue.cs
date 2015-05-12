namespace Daishi.AMQP {
    public abstract class AMQPQueue {
        public string Name { get; set; }
        public bool IsNew { get; set; }

        public override string ToString() {
            return Name;
        }
    }
}