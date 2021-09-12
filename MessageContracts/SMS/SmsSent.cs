﻿using MassTransit.Topology;

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
    }
}