using MassTransit;
using MassTransit.Topology;

using MessageComponets.SmsConsumers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace SmsMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(cfg =>
                    {
                     
                        // Add all consumers in the specified assembly
                       // cfg.AddConsumers(typeof(SendSmsConsumer).Assembly);

                        cfg.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.ConfigureEndpoints(context);
                        });
                    });
                    services.AddMassTransitHostedService();
                    services.AddHostedService<Worker>();
                });
    }
  
}
