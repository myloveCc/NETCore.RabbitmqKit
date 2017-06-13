using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using NETCore.RabbitmqKit.Shared;
using RabbitMQ.Client.Exceptions;

namespace NETCore.RabbitmqKit.Core
{
    /// <summary>
    /// Rabbitmq queue
    /// </summary>
    public class RabbitmqQueue : IRabbitmqQueue
    {
        private IRabbitmqProvider _RabbitmqProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rabbitmqProvider"><see cref="IRabbitmqProvider"/></param>
        public RabbitmqQueue(IRabbitmqProvider rabbitmqProvider)
        {
            _RabbitmqProvider = rabbitmqProvider;
        }

        /// <summary>
        /// queue info
        /// </summary>
        /// <param name="queueName">is queueName</param>
        /// <param name="isDurable">is durable</param>
        /// <param name="isExclusive">is exclusive</param>
        /// <param name="isAutoDelete">is auto delete</param>
        /// <param name="arguments">queue arguments</param>
        /// <returns><see cref="QueueDeclareOk"/></returns>
        public QueueDeclareOk QueueInfo(string queueName, bool isDurable, bool isExclusive, bool isAutoDelete = false, IDictionary<string, object> arguments = null)
        {
            Check.Argument.IsNotEmpty(queueName, nameof(queueName));

            using (IConnection collection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = collection.CreateModel())
                {
                    try
                    {
                        return channel.QueueDeclare(queueName, isDurable, isExclusive, isAutoDelete, arguments);
                    }
                    catch (Exception ex)
                    {
                        //TODO log exception

                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// queue bind to exchange
        /// </summary>
        /// <param name="queueName">queue name</param>
        /// <param name="exchangeName">exchange name</param>
        /// <param name="routingKey">routing key</param>
        /// <param name="arguments">arguments,default is null</param>
        public void QueueBind(string queueName, string exchangeName, string routingKey, IDictionary<string, object> arguments = null)
        {
            Check.Argument.IsNotEmpty(queueName, nameof(queueName));
            Check.Argument.IsNotEmpty(exchangeName, nameof(exchangeName));

            using (IConnection collection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = collection.CreateModel())
                {
                    channel.QueueBind(queueName, exchangeName, routingKey, arguments);
                }
            }
        }

        /// <summary>
        /// queue un bind from exchange
        /// </summary>
        /// <param name="queueName">queue name</param>
        /// <param name="exchangeName">exchange name</param>
        /// <param name="routingKey">routing key</param>
        /// <param name="arguments">arguments,default is null</param>
        public void QueueUnBind(string queueName, string exchangeName, string routingKey, IDictionary<string, object> arguments = null)
        {
            Check.Argument.IsNotEmpty(queueName, nameof(queueName));
            Check.Argument.IsNotEmpty(exchangeName, nameof(exchangeName));

            using (IConnection collection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = collection.CreateModel())
                {
                    channel.QueueUnbind(queueName, exchangeName, routingKey, arguments);
                }
            }
        }

        /// <summary>
        /// clear queue,not delete
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns>Returns the number of messages during queue clear.</returns>
        public uint QueueClear(string queueName)
        {
            Check.Argument.IsNotEmpty(queueName, nameof(queueName));
            using (IConnection collection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = collection.CreateModel())
                {
                    return channel.QueuePurge(queueName);
                }
            }
        }

        /// <summary>
        /// queue delete and clear messages
        /// </summary>
        /// <param name="queueName">the delete queue name</param>
        /// <returns>Returns the number of messages purged  during queue delete.</returns>
        public uint QueueDelete(string queueName)
        {
            Check.Argument.IsNotEmpty(queueName, nameof(queueName));
            using (IConnection collection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = collection.CreateModel())
                {
                    return channel.QueueDelete(queueName);
                }
            }
        }

        /// <summary>
        /// queue delete
        /// </summary>
        /// <param name="queueuName">the delete queue name</param>
        /// <param name="isUnUsed">delete queue if un used,</param>
        /// <param name="isEmpty">delete queue if empty, default value is true</param>
        /// <returns>Returns the number of messages purged  during queue delete.</returns>
        public uint QueueDelete(string queueName, bool isUnUsed, bool isEmpty = true)
        {
            Check.Argument.IsNotEmpty(queueName, nameof(queueName));
            using (IConnection collection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = collection.CreateModel())
                {
                    return channel.QueueDelete(queueName, isUnUsed, isEmpty);
                }
            }
        }
    }
}
