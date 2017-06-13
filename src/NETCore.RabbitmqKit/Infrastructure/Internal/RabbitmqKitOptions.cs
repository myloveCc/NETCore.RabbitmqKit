using System;
using System.Collections.Generic;
using System.Text;
using NETCore.RabbitmqKit.Shared;

namespace NETCore.RabbitmqKit.Infrastructure
{
    public class RabbitmqKitOptions : IRabbitmqKitOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public string ClientProvidedName { get; private set; } = string.Empty;

        /// <summary>
        /// Rabbitmq  host name collection
        /// </summary>
        public ICollection<string> HostNames { get; }

        /// <summary>
        /// Rabbitmq user account
        /// </summary>
        public string UserName { get; private set; } = "guest";

        /// <summary>
        /// Rabbitmq user password
        /// </summary>
        public string Password { get; private set; } = "guest";

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="hostNames">Rabbitmq host name collections</param>
        public RabbitmqKitOptions(ICollection<string> hostNames)
        {
            Check.Argument.IsNotEmpty(hostNames, nameof(hostNames));
            this.HostNames = hostNames;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="hostNames">Rabbitmq host name collections</param>
        /// <param name="clientProvidedName"> Rabbitmq client provided name</param>
        public RabbitmqKitOptions(ICollection<string> hostNames, string clientProvidedName)
        {
            Check.Argument.IsNotEmpty(hostNames, nameof(hostNames));
            Check.Argument.IsNotEmpty(clientProvidedName, nameof(clientProvidedName));

            this.HostNames = hostNames;
            this.ClientProvidedName = clientProvidedName;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="hostName">Rabbitmq host name</param>
        /// <param name="userName">Rabbitmq user account</param>
        /// <param name="password">Rabbitmq user password</param>
        public RabbitmqKitOptions(ICollection<string> hostNames, string userName, string password) : this(hostNames)
        {
            this.UserName = userName;
            this.Password = password;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="hostName">Rabbitmq host name</param>
        /// <param name="clientProvidedName"> Rabbitmq client provided name</param>
        /// <param name="userName">Rabbitmq user account</param>
        /// <param name="password">Rabbitmq user password</param>
        public RabbitmqKitOptions(ICollection<string> hostNames, string clientProvidedName, string userName, string password) : this(hostNames, clientProvidedName)
        {
            this.UserName = userName;
            this.Password = password;
        }
    }
}
