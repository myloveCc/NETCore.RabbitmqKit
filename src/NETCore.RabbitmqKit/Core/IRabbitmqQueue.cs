using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.RabbitmqKit.Core
{
    public interface IRabbitmqQueue
    {
        /// <summary>
        /// queue info
        /// </summary>
        /// <param name="queueName">is queueName</param>
        /// <param name="isDurable">is durable</param>
        /// <param name="isExclusive">is exclusive</param>
        /// <param name="isAutoDelete">is auto delete</param>
        /// <param name="arguments">queue arguments</param>
        /// <returns><see cref="QueueDeclareOk"/></returns>
        QueueDeclareOk QueueInfo(string queueName, bool isDurable, bool isExclusive, bool isAutoDelete, IDictionary<string, object> arguments);

        /// <summary>
        /// queue bind to exchange
        /// </summary>
        /// <param name="queueName">queue name</param>
        /// <param name="exchangeName">exchange name</param>
        /// <param name="routingKey">routing key</param>
        /// <param name="arguments">arguments,default is null</param>
        void QueueBind(string queueName, string exchangeName, string routingKey, IDictionary<string, object> arguments = null);

        /// <summary>
        /// queue un bind from exchange
        /// </summary>
        /// <param name="queueName">queue name</param>
        /// <param name="exchangeName">exchange name</param>
        /// <param name="routingKey">routing key</param>
        /// <param name="arguments">arguments,default is null</param>
        void QueueUnBind(string queueName, string exchangeName, string routingKey, IDictionary<string, object> arguments = null);

        /// <summary>
        /// queue delete 
        /// </summary>
        /// <param name="queueName">the delete queue name</param>
        /// <returns>Returns the number of messages purged  during queue delete.</returns>
        uint QueueDelete(string queueName);

        /// <summary>
        /// queue delete
        /// </summary>
        /// <param name="queueuName">the delete queue name</param>
        /// <param name="isUnUsed">delete queue if un used,</param>
        /// <param name="isEmpty">delete queue if empty, default value is true</param>
        /// <returns>Returns the number of messages purged  during queue delete.</returns>
        uint QueueDelete(string queueuName, bool isUnUsed, bool isEmpty = true);

        /// <summary>
        /// clear queue
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns>Returns the number of messages during queue clear.</returns>
        uint QueueClear(string queueName);
    }
}
