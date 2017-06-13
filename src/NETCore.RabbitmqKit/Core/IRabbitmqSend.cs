using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NETCore.RabbitmqKit.Core
{
    public interface IRabbitmqSend
    {
        #region Sync

        /// <summary>
        /// send message
        /// </summary>
        /// <param name="sendAction"><see cref="Action{T}"/></param>
        void Send(Action<IModel> sendAction);

        #endregion

        #region Async

        /// <summary>
        ///  send message async
        /// </summary>
        /// <param name="sendAction"><see cref="Action"/></param>
        /// <returns></returns>
        Task SendAsnyc(Action<IModel> sendAction);

        /// <summary>
        ///  send message async
        /// </summary>
        /// <param name="sendAction"><see cref="Action"/></param>
        /// <param name="cancellationToken"><see cref="CancellationTokenSource"/></param>
        /// <returns></returns>
        Task SendAsnyc(Action<IModel> sendAction, CancellationTokenSource cancellationTokenSource);

        #endregion

    }
}
