using MassTransit;
using RabbitMQ.MassTransit.Shared.Messages;

string localhost = "rabbitmq://localhost";
string queueName = "sample_queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(localhost);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new Uri($"{localhost}/{queueName}"));
Console.Write("Sending Message: ");
string message = Console.ReadLine();

await sendEndpoint.Send<IMessage>(new SampleMessage
{
    Text = message
});

Console.Read();