using MassTransit;
using RabbitMQ.Masstransit.RequestResponsePattern.Consumer.Consumers;

Console.WriteLine("Starting RabbitMQ Request-Response Pattern Consumer...");

string localhost = "rabbitmq://localhost";
string queueName = "sample_request_response_queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(localhost);
    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<RequestResponsePatternConsumer>();
    });
});

await bus.StartAsync();

Console.Read();