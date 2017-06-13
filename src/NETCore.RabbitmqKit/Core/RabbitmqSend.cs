using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NETCore.RabbitmqKit;
using RabbitMQ.Client;

namespace NETCore.RabbitmqKit.Core
{
    /// <summary>
    ///  send  message service
    /// </summary>
    public class RabbitmqSend : IRabbitmqSend
    {
        private IRabbitmqProvider _RabbitmqProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rabbitmqProvider"><see cref="IRabbitmqProvider"/></param>
        public RabbitmqSend(IRabbitmqProvider rabbitmqProvider)
        {
            _RabbitmqProvider = rabbitmqProvider;
        }

        /// <summary>
        /// send message
        /// </summary>
        /// <param name="sendAction"><see cref="Action"/></param>
        public void Send(Action<IModel> sendAction)
        {
            using (IConnection collection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = collection.CreateModel())
                {
                    sendAction.Invoke(channel);
                }
            }
        }

        /// <summary>
        ///  send message async
        /// </summary>
        /// <param name="sendAction"><see cref="Action"/></param>
        /// <returns></returns>
        public Task SendAsnyc(Action<IModel> sendAction)
        {
            return Task.Factory.StartNew(() =>
            {
                using (IConnection collection = _RabbitmqProvider.CreateCollection())
                {
                    using (IModel channel = collection.CreateModel())
                    {
                        sendAction.Invoke(channel);
                    }
                }
            });
        }

        /// <summary>
        ///  send message async width tokensource
        /// </summary>
        /// <param name="sendAction"><see cref="Action"/></param>
        /// <param name="cancellationToken"><see cref="CancellationTokenSource"/></param>
        /// <returns></returns>
        public Task SendAsnyc(Action<IModel> sendAction, CancellationTokenSource cancellationTokenSource)
        {
            return Task.Factory.StartNew(() =>
                   {
                       using (IConnection collection = _RabbitmqProvider.CreateCollection())
                       {
                           using (IModel channel = collection.CreateModel())
                           {
                               sendAction.Invoke(channel);
                           }
                       }
                   },
                   cancellationTokenSource.Token);

        }
    }
}
