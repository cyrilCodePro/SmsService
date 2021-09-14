using MassTransit;

using MessageComponets.ApiService;

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
        private readonly IApiSmsService _apiSmsService;



        public SendSmsConsumer(ILogger<SendSmsConsumer> logger, IApiSmsService smsService)
        {
            _logger = logger;
            _apiSmsService = smsService;
        
        }

        public async Task Consume(ConsumeContext<SendSms> context)
        {
            _logger.LogInformation("Received Text: {PhoneNumber}", context.Message.PhoneNumber);
            await  _apiSmsService.SendSmsToApi(new { context.Message.PhoneNumber, context.Message.SmsText });
            _logger.LogInformation("Sms Sent Successfully");
          }
    }
}
