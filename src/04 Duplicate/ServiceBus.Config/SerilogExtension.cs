using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Config
{
    public class SerilogExtension
    {
        public void AddSerilog(IServiceCollection services, IConfiguration configuration) {
            var logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger()
                          .ForContext(GetType());
            logger.Information("Logger created");
        }
    }
}
