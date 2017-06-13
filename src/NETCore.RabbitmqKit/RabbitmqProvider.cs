using NETCore.RabbitmqKit.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using System.Linq;
using NETCore.RabbitmqKit.Shared;

namespace NETCore.RabbitmqKit
{
    public class RabbitmqProvider : IRabbitmqProvider
    {
        /// <summary>
        /// rediskit options
        /// </summary>
        private readonly IRabbitmqKitOptions _RabbitmqKitOptions;
        private readonly ConnectionFactory _ConnectionFactory;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"><see cref="IRabbitmqKitOptions"/></param>
        public RabbitmqProvider(IRabbitmqKitOptions options)
        {
            Check.Argument.IsNotEmpty(options.HostNames, nameof(options.HostNames));

            _RabbitmqKitOptions = options;
            _ConnectionFactory = new ConnectionFactory()
            {
                UserName = options.UserName,
                Password = options.Password
            };
        }

        /// <summary>
        /// create rabbitmq collection
        /// </summary>
        /// <returns></returns>
        public IConnection CreateCollection()
        {
            var hostNames = _RabbitmqKitOptions.HostNames.ToList();

            if (string.IsNullOrEmpty(_RabbitmqKitOptions.ClientProvidedName))
            {
                return _ConnectionFactory.CreateConnection(hostNames);
            }
            else
            {
                return _ConnectionFactory.CreateConnection(hostNames, _RabbitmqKitOptions.ClientProvidedName);
            }
        }
    }
}
