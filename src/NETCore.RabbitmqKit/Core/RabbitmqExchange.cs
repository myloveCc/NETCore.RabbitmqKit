using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RabbitMQ.Client;
using NETCore.RabbitmqKit.Shared;

namespace NETCore.RabbitmqKit.Core
{
    /// <summary>
    /// Rabbitmq exchange
    /// </summary>
    public class RabbitmqExchange : IRabbitmqExchange
    {
        private IRabbitmqProvider _RabbitmqProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rabbitmqProvider"><see cref="IRabbitmqProvider"/></param>
        public RabbitmqExchange(IRabbitmqProvider rabbitmqProvider)
        {
            _RabbitmqProvider = rabbitmqProvider;
        }

        /// <summary>
        /// exchange bind to another exchange
        /// </summary>
        /// <param name="destinationExchange">destination exchange</param>
        /// <param name="sourceExchange">source exchange</param>
        /// <param name="routingKey">routing key</param>
        /// <param name="arguments">arguments,default is null</param>
        public void ExchangeBind(string destinationExchange, string sourceExchange, string routingKey = "", IDictionary<string, object> arguments = null)
        {
            Check.Argument.IsNotEmpty(destinationExchange, nameof(destinationExchange));
            Check.Argument.IsNotEmpty(sourceExchange, nameof(sourceExchange));

            using (IConnection connection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeBind(destinationExchange, sourceExchange, routingKey, arguments);
                }
            }
        }

        /// <summary>
        /// Exchange declare
        /// </summary>
        /// <param name="exchangeName">exchange name</param>
        /// <param name="exchangeType">exchange type <see cref="ExchangeType"/></param>
        /// <param name="isDurable">is durable</param>
        /// <param name="isAutoDelete">is auto delete</param>
        /// <param name="arguments">arguments,default is null</param>
        public void ExchangeDeclare(string exchangeName, string exchangeType, bool isDurable, bool isAutoDelete = false, IDictionary<string, object> arguments = null)
        {
            Check.Argument.IsNotEmpty(exchangeName, nameof(exchangeName));
            Check.Argument.IsNotEmpty(exchangeName, nameof(exchangeType));

            exchangeType = exchangeType.ToLower();
            if (!ExchangeType.All().Contains(exchangeType))
            {
                Check.Argument.IsNotValid(exchangeType, nameof(exchangeType));
            }

            using (IConnection connection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchangeName, exchangeType, isDurable, isAutoDelete, arguments);
                }
            }
        }

        /// <summary>
        /// Exchange delete
        /// </summary>
        /// <param name="exchangeName">exchange name</param>
        /// <param name="ifUnused">delete if un used,default is "true"</param>
        public void ExchangeDelete(string exchangeName, bool ifUnused = true)
        {
            Check.Argument.IsNotEmpty(exchangeName, nameof(exchangeName));
            using (IConnection connection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDelete(exchangeName, ifUnused);
                }
            }
        }

        /// <summary>
        /// exchange un bind
        /// </summary>
        /// <param name="destinationExchange">destination exchange</param>
        /// <param name="sourceExchange">source exchange</param>
        /// <param name="routingKey">routing key</param>
        /// <param name="arguments">arguments,default is null</param>
        public void ExchangeUnbind(string destinationExchange, string sourceExchange, string routingKey, IDictionary<string, object> arguments = null)
        {
            Check.Argument.IsNotEmpty(destinationExchange, nameof(destinationExchange));
            Check.Argument.IsNotEmpty(sourceExchange, nameof(sourceExchange));

            using (IConnection connection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeUnbind(destinationExchange, sourceExchange, routingKey, arguments);
                }
            }
        }
    }
}
