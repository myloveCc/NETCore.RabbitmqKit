using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NETCore.RabbitmqKit.Core
{
    public interface IRabbitmqReceive
    {
        #region Sync

        /// <summary>
        /// receive message
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="receiveAction"><see cref="Action{T}"/></param>
        /// <returns></returns>
        void Receive(Action<IModel> receiveAction);

        #endregion

        #region Async

        /// <summary>
        /// receive message
        /// </summary>
        /// <param name="receiveAction"><see cref="Action{T}"/></param>
        /// <returns></returns>
        Task ReceiveAsync(Action<IModel> receiveAction) ;

        /// <summary>
        ///  receive message async with cancel token source
        /// </summary>
        /// <param name="receiveAction"><see cref="Action{T}"/></param>
        /// <param name="cancellationToken"><see cref="CancellationTokenSource"/></param>
        /// <returns></returns>
        Task ReceiveAsync(Action<IModel> receiveAction, CancellationTokenSource cancellationTokenSource);

        #endregion
    }
}
