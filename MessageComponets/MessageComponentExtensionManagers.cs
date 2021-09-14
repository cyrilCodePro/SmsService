using MessageComponets.ApiService;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageComponets
{
  public static  class MessageComponentExtensionManagers
    {
        public static IServiceCollection AddMessageComponetsExtensions( this IServiceCollection services)
        {
            services.AddTransient<IApiSmsService, ApiSmsService>();
            return services;
        }
    }
}
