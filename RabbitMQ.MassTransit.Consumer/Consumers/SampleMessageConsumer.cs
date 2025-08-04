using MassTransit;
using RabbitMQ.MassTransit.Shared.Messages;

namespace RabbitMQ.MassTransit.Consumer.Consumers
{
    public class SampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Received Message: {context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}