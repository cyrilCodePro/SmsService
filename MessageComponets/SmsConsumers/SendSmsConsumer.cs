using MassTransit;

using MessageContracts.SMS;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageComponets.SmsConsumers
{
   public class SendSmsConsumer:IConsumer<SendSms>
    {
        private readonly ILogger<SendSmsConsumer> _logger;
      

        public SendSmsConsumer(ILogger<SendSmsConsumer> logger)
        {
            _logger = logger;
        
        }

        public async Task Consume(ConsumeContext<SendSms> context)
        {
            _logger.LogInformation("Received Text: {PhoneNumber}", context.Message.PhoneNumber);
           await context.Publish<SmsSent>(new
            {
               message="Sent Successfully"
           });

          
        }
    }
}
