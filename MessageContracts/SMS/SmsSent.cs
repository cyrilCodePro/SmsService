using MassTransit.Topology;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts.SMS
{
    [EntityName("SmsSent")]
    public interface SmsSent
    {
        string message { get; set; }
        Guid IdempotenceKey { get; set; }
    }

    public class SmsSentEvent : SmsSent
    {
        public SmsSentEvent(string message, Guid id)
        {
            this.message = message;
            IdempotenceKey = id;
        }

        public string message { get; set; }
        public Guid IdempotenceKey { get; set; }
    }
}
