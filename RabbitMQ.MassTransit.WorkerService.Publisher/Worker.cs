using MassTransit;
using RabbitMQ.MassTransit.Shared.Messages;

namespace RabbitMQ.MassTransit.WorkerService.Publisher
{
    public class Worker : BackgroundService
    {
        readonly IPublishEndpoint _publishEndpoint;

        public Worker(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;

            while (true)
            {
                SampleMessage message = new SampleMessage
                {
                    Text = $"Message {i++} at {DateTime.Now}"
                };

                await _publishEndpoint.Publish<IMessage>(message, stoppingToken)
                    .ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Console.WriteLine($"Error publishing message: {task.Exception?.GetBaseException().Message}");
                        }
                    }, stoppingToken);
                Thread.Sleep(1000); // Sleep for 1 second before sending the next message
                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}
