using MassTransit.Topology;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContracts.SMS
{
    [EntityName("SendSms")]
   public interface SendSms
    {
        string PhoneNumber { get; set; }
        string SmsText { get; set; }
        Guid? IdempotenceKey { get; set; }
    }
}
