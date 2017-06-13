using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.RabbitmqKit
{
    public interface IRabbitmqProvider
    {
        /// <summary>
        /// create rabbitmq collection
        /// </summary>
        /// <returns></returns>
        IConnection CreateCollection();
    }
}
