using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageComponets.ApiService
{
    public class ApiSmsService : IApiSmsService
    {
        private readonly ILogger<ApiSmsService> _logger;

        public ApiSmsService(ILogger<ApiSmsService> logger)
        {
            _logger = logger;
        }

        public async Task SendSmsToApi(object payload)
        {
            await Task.Delay(1000);
            _logger.LogInformation("Sms Api sent to third party");
        }
    }
}
