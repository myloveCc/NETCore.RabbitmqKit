using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace NETCore.RabbitmqKit.Core
{
    public interface IRabbitmqExchange
    {
        /// <summary>
        /// exchange bind
        /// </summary>
        /// <param name="destinationExchange">destination exchange</param>
        /// <param name="sourceExchange">source exchange</param>
        /// <param name="routingKey">routing key</param>
        /// <param name="arguments">arguments,default is null</param>
        void ExchangeBind(string destinationExchange, string sourceExchange, string routingKey, IDictionary<string, object> arguments = null);


        /// <summary>
        /// Exchange declare
        /// </summary>
        /// <param name="exchangeName">exchange name</param>
        /// <param name="exchangeType">exchange type <see cref="ExchangeType"/></param>
        /// <param name="isDurable">is durable</param>
        /// <param name="isAutoDelete">is auto delete</param>
        /// <param name="arguments">arguments,default is null</param>
        void ExchangeDeclare(string exchangeName, string exchangeType, bool isDurable, bool isAutoDelete = false, IDictionary<string, object> arguments = null);


        /// <summary>
        /// Exchange delete
        /// </summary>
        /// <param name="exchangeName">exchange name</param>
        /// <param name="ifUnused">delete if un used,default is "true"</param>
        void ExchangeDelete(string exchangeName, bool ifUnused = true);

        /// <summary>
        /// exchange un bind
        /// </summary>
        /// <param name="destinationExchange">destination exchange</param>
        /// <param name="sourceExchange">source exchange</param>
        /// <param name="routingKey">routing key</param>
        /// <param name="arguments">arguments,default is null</param>
        void ExchangeUnbind(string destinationExchange, string sourceExchange, string routingKey, IDictionary<string, object> arguments);
    }
}
