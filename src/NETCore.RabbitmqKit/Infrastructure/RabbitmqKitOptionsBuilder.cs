using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NETCore.RabbitmqKit.Core;
using NETCore.RabbitmqKit.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.RabbitmqKit.Infrastructure
{
    public class RabbitmqKitOptionsBuilder : IRabbitmqKitOptionsBuilder
    {
        /// <summary>
        /// Gets the service collection in which the interception based services are added.
        /// </summary>
        public IServiceCollection serviceCollection { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> service collection</param>
        public RabbitmqKitOptionsBuilder(IServiceCollection services)
        {
            this.serviceCollection = services;
        }

        /// <summary>
        /// use rabbitma
        /// </summary>
        /// <param name="options">rabbitmq options</param>
        /// <param name="lifetime">sevice left time</param>
        /// <returns></returns>
        public IRabbitmqKitOptionsBuilder UseRabbitMQ(RabbitmqKitOptions options, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Check.Argument.IsNotNull(options, nameof(options), "The rabbitmq options is null");

            AddProviderService(options);

            serviceCollection.TryAdd(new ServiceDescriptor(typeof(IRabbitmqQueue), typeof(RabbitmqQueue), lifetime));
            serviceCollection.TryAdd(new ServiceDescriptor(typeof(IRabbitmqExchange), typeof(RabbitmqExchange), lifetime));
            serviceCollection.TryAdd(new ServiceDescriptor(typeof(IRabbitmqSend), typeof(RabbitmqSend), lifetime));
            serviceCollection.TryAdd(new ServiceDescriptor(typeof(IRabbitmqReceive), typeof(RabbitmqReceive), lifetime));
            return this;
        }

        /// <summary>
        /// add core service 
        /// </summary>
        /// <param name="options"></param>
        private void AddProviderService(RabbitmqKitOptions options)
        {
            RabbitmqProvider provider = new RabbitmqProvider(options);

            //serviceCollection.TryAddSingleton<IRabbitmqKitOptions>(options);
            serviceCollection.TryAddSingleton<IRabbitmqProvider>(provider);
        }
    }
}
