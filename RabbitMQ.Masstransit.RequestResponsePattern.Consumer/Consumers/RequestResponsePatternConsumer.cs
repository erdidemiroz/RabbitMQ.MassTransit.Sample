using MassTransit;
using RabbitMQ.MassTransit.Shared.RequestResponseMessages;

namespace RabbitMQ.Masstransit.RequestResponsePattern.Consumer.Consumers
{
    public class RequestResponsePatternConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            Console.WriteLine(context.Message.Text);
            await context.RespondAsync<ResponseMessage>(new ResponseMessage
            {
                Text = $"{context.Message.MessageNo}. response to request"
            });
        }
    }
}
