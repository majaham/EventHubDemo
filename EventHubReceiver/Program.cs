using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Storage.Blobs;
using System;
using System.Threading.Tasks;

namespace EventHubReceiver
{
    class Program
    {
        //You will need to create event hubs namespace and subscription
        private static readonly string connString = "";
        private static readonly string consumerGroup = "$Default";
        private static readonly string connStorage = "";
        private static readonly string containerName = "event-checkpoints";

        static async Task Main(string[] args)
        {
            BlobContainerClient blobClient = new BlobContainerClient(connStorage, containerName);
            EventProcessorClient _processor = new EventProcessorClient(blobClient, consumerGroup, connString);

            _processor.ProcessEventAsync += Processor_ProcessEventAsync;
            _processor.ProcessErrorAsync += Processor_ProcessErrorAsync;

            await _processor.StartProcessingAsync();

            await Task.Delay(TimeSpan.FromSeconds(30));

            Console.ReadKey();
        }

        private static Task Processor_ProcessErrorAsync(Azure.Messaging.EventHubs.Processor.ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.Message);
            return Task.CompletedTask;
        }

        private static async Task Processor_ProcessEventAsync(Azure.Messaging.EventHubs.Processor.ProcessEventArgs arg)
        {
            Console.WriteLine(arg.Data.EventBody.ToString());
            await arg.UpdateCheckpointAsync(arg.CancellationToken);
        }
    }
}
