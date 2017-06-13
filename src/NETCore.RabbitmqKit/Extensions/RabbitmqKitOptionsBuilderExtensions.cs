using Microsoft.Extensions.DependencyInjection;
using NETCore.RabbitmqKit.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using NETCore.RabbitmqKit.Shared;

namespace NETCore.RabbitmqKit.Extensions
{
    public static class RabbitmqKitOptionsBuilderExtensions
    {
        public static IRabbitmqKitOptionsBuilder UseRabbitMQ(IRabbitmqKitOptionsBuilder builder, RabbitmqKitOptions options, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Check.Argument.IsNotNull(builder, nameof(builder), "The RabbitmqKitOptionBuilder is null");
            Check.Argument.IsNotNull(options, nameof(options), "The RabbitmqKitOptions is null");

            return builder.UseRabbitMQ(options, lifetime);
        }
    }
}
