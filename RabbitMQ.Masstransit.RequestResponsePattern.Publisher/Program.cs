using MassTransit;
using RabbitMQ.MassTransit.Shared.RequestResponseMessages;

Console.WriteLine("Starting RabbitMQ Request-Response Pattern Publisher...");

string localhost = "rabbitmq://localhost";
string queueName = "sample_request_response_queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(localhost);
});

await bus.StartAsync();

var request = bus.CreateRequestClient<RequestMessage>(new Uri($"{localhost}/{queueName}"));

int i = 1;

while (true)
{
    await Task.Delay(500);

    var response = await request.GetResponse<ResponseMessage>(new RequestMessage
    {
        MessageNo = i,
        Text = $"Request {i++}"
    });

    Console.WriteLine($"Response received:{response.Message.Text}");
}