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
                .Publish(context => (SmsSent)new SmsSentEvent("sms sent succefully",context.Instance.CorrelationId))
                 .Then(x => logger?.LogInformation("Message sent and event published the idempotence key for the event is {Id}",x.Instance.CorrelationId))
                 .TransitionTo(SmsSubmitted));

            During(SmsSubmitted,
                When(SendSms)
                    .Then(x => logger?.LogInformation("Sms already sent")));



        }
        public void ConfigureCorrelationIds()
        {
            Event(() => SendSms, x => x.CorrelateById(m => m.Message.IdempotenceKey));
         }
        
     
      public State SmsSubmitted { get; set; }
      public Event<SendSms> SendSms { get; private set; }


    }
}
