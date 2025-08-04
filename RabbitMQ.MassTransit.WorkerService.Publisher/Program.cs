using MassTransit;
using RabbitMQ.MassTransit.WorkerService.Publisher;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("rabbitmq://localhost");
            });
        });

        services.AddHostedService<Worker>(provider =>
        {
            using var scope = provider.CreateScope();
            var publishEndpoint = scope.ServiceProvider.GetService<IPublishEndpoint>();
            return new(publishEndpoint);
        });
    }).Build();

await host.RunAsync();