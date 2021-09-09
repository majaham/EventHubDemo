using System.Text.Json;

namespace EventHubSender
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
