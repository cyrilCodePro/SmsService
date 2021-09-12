using MassTransit;

using MessageContracts.SMS;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmsMicroservice
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        readonly IBus _bus;

        public Worker(ILogger<Worker> logger,IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish<SendSms>(new
                {
                    PhoneNumber = "+254702831844",
                    SmsText = "Hey",
                    IdempotenceKey = Guid.NewGuid()

                });


                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
            }
        }
    }
}
