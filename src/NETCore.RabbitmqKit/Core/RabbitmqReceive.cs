using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace NETCore.RabbitmqKit.Core
{
    /// <summary>
    /// receive message service 
    /// </summary>
    public class RabbitmqReceive : IRabbitmqReceive
    {
        private IRabbitmqProvider _RabbitmqProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rabbitmqProvider"><see cref="IRabbitmqProvider"/></param>
        public RabbitmqReceive(IRabbitmqProvider rabbitmqProvider)
        {
            _RabbitmqProvider = rabbitmqProvider;
        }

        /// <summary>
        /// receive message
        /// </summary>
        /// <param name="receiveAction"><see cref="Action{T}"/></param>
        /// <returns></returns>
        public void Receive(Action<IModel> receiveAction)
        {
            using (IConnection collection = _RabbitmqProvider.CreateCollection())
            {
                using (IModel channel = collection.CreateModel())
                {
                    receiveAction.Invoke(channel);
                    Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// receive message async
        /// </summary>
        /// <param name="receiveAction"><see cref="Action{T}"/></param>
        /// <returns></returns>
        public Task ReceiveAsync(Action<IModel> receiveAction)
        {
            return Task.Factory.StartNew(() =>
            {
                using (IConnection collection = _RabbitmqProvider.CreateCollection())
                {
                    using (IModel channel = collection.CreateModel())
                    {
                        receiveAction.Invoke(channel);
                        Console.ReadLine();
                    }
                };
            });
        }

        /// <summary>
        /// receive message async with cancle token source
        /// </summary>
        /// <param name="receiveAction"><see cref="Action{T}"/></param>
        /// <param name="cancellationTokenSource"><see cref="CancellationTokenSource"/></param>
        /// <returns></returns>
        public Task ReceiveAsync(Action<IModel> receiveAction, CancellationTokenSource cancellationTokenSource)
        {
            return Task.Factory.StartNew(() =>
            {
                using (IConnection collection = _RabbitmqProvider.CreateCollection())
                {
                    using (IModel channel = collection.CreateModel())
                    {
                        receiveAction.Invoke(channel);
                        Console.ReadLine();
                    }
                };
            }, cancellationTokenSource.Token);
        }
    }
}
