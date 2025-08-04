using MassTransit;
using RabbitMQ.Masstransit.WorkerService.Consumer.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<SampleMessageConsumer>();
            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost");
                cfg.ReceiveEndpoint("sample_queue", e => e.ConfigureConsumer<SampleMessageConsumer>(context));
            });
        });
    }).Build();

await host.RunAsync();