using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventHubSender
{
    class Program
    {
        private static readonly string connString = "";

        static async Task Main(string[] args)
        {
            List<Order> _orders = new List<Order>
            {
                new Order{ OrderId=1000, Category="Mobile", Quantity=10, Price=120.95},
                new Order{ OrderId=1001, Category="Computer", Quantity=15, Price=130.50},
                new Order{ OrderId=1002, Category="Mobile", Quantity=20, Price=150.90},
                new Order{ OrderId=1003, Category="Computer", Quantity=10, Price=1200.00}
            };

            EventHubProducerClient _client = new EventHubProducerClient(connString);
            EventDataBatch _batch = await _client.CreateBatchAsync();

            foreach(var _order in _orders)
            {
                Console.ReadKey();
                EventData data = new EventData(_order.ToString());
                _batch.TryAdd(data);
                await _client.SendAsync(_batch);
                
            }
            

            Console.WriteLine("All messages sent");
        }
    }
}
