using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.RabbitmqKit.Infrastructure
{
    public interface IRabbitmqKitOptionsBuilder
    {
        /// <summary>
        /// service collection
        /// </summary>
        IServiceCollection serviceCollection { get; }

        /// <summary>
        /// get redis options and add ConnectionMultiplexer to sercice collection
        /// </summary>
        /// <param name="options">redis options</param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        IRabbitmqKitOptionsBuilder UseRabbitMQ(IRabbitmqKitOptions options, ServiceLifetime lifetime = ServiceLifetime.Scoped);
    }
}
