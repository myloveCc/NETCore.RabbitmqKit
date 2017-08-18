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
        /// add rabbitmq service
        /// </summary>
        /// <param name="options">rabbitmq options</param>
        /// <param name="lifetime"><see cref="ServiceLifetime"/></param>
        /// <returns></returns>
        IRabbitmqKitOptionsBuilder UseRabbitMQ(RabbitmqKitOptions options, ServiceLifetime lifetime = ServiceLifetime.Scoped);
    }
}
