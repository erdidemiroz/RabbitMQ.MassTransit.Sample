using MassTransit;
using RabbitMQ.MassTransit.Shared.Messages;

namespace RabbitMQ.MassTransit.WorkerService.Publisher
{
    public class Worker : BackgroundService
    {
        private readonly IPublishEndpoint _publishEndpoint;

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
                    Text = $"Message {i++} at {DateTime.Now:MM/dd/yyyy hh:mm:ss.fff}"
                };

                await _publishEndpoint.Publish<IMessage>(message, stoppingToken)
                    .ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Console.WriteLine($"Error publishing message: {task.Exception?.GetBaseException().Message}");
                        }
                    }, stoppingToken);
                Thread.Sleep(500);
                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}
