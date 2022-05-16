using System;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace StorageQueueDemo
{
    class Program
    {
        private const string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=storagequeuevrb;AccountKey=;EndpointSuffix=core.windows.net";
        private const string queueName = "store-messages";

        static void Main(string[] args)
        {
            // Instantiate a QueueClient which will be used to create and manipulate the queue
            QueueClient queueClient = new QueueClient(storageConnectionString, queueName);

            if (queueClient.Exists())
            {
                while (true)
                {
                    // Get the next message
                    QueueMessage[] retrievedMessages = queueClient.ReceiveMessages(1);

                    if (retrievedMessages.Length > 0)
                    {
                        // Process (i.e. print) the message in less than 30 seconds
                        Console.WriteLine($"Dequeued message: '{retrievedMessages[0].MessageText}'");

                        // Delete the message
                        queueClient.DeleteMessage(retrievedMessages[0].MessageId, retrievedMessages[0].PopReceipt);
                    }
                }
            }
        }
    }
}
