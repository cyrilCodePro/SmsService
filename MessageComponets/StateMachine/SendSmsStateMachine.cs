using Automatonymous;

using MessageContracts.SMS;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageComponets.StateMachine
{
 

    public class SendSmsStateMachine : MassTransitStateMachine<SendSmsState>
    {
        public SendSmsStateMachine(ILogger<SendSmsStateMachine> logger)
        {
            this.InstanceState(x => x.CurrentState);
            this.ConfigureCorrelationIds();
           
            Initially(
                When(SendSms)
                .Publish(context => (SmsSent)new SmsSentEvent("sms sent succefully"))
                 .Then(x => logger.LogInformation("Here is hit"))) ;

        }
        public void ConfigureCorrelationIds()
        {
            Event(() => SendSms,
    x => x.CorrelateById(x => x.Message.IdempotenceKey)
       .SelectId(c => c.Message.IdempotenceKey));

        }
        
     
      public State SmsSent { get; private set; }
      public Event<SendSms> SendSms { get; private set; }


    }
}
