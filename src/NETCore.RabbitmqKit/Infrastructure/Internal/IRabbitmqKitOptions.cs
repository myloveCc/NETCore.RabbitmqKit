using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.RabbitmqKit.Infrastructure
{
    public interface IRabbitmqKitOptions
    {
        /// <summary>
        /// Client provider name
        /// </summary>
        string ClientProvidedName { get; }

        /// <summary>
        /// Rabbitmq  host name collection
        /// </summary>
        ICollection<string> HostNames { get; }

        /// <summary>
        /// Rabbitmq user account
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Rabbitmq user password
        /// </summary>
        string Password { get; }
    }
}
