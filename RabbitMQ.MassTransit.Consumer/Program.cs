using MassTransit;
using RabbitMQ.MassTransit.Consumer.Consumers;

string localhost = "rabbitmq://localhost";
string queueName = "sample_queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(localhost);
    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<SampleMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();