using Microsoft.Extensions.DependencyInjection;
using NETCore.RabbitmqKit.Infrastructure;
using NETCore.RabbitmqKit.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore.RabbitmqKit.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddRabbitmqKit(this IServiceCollection serviceCollection, Action<RabbitmqKitOptionsBuilder> optionsAction)
        {
            Check.Argument.IsNotNull(serviceCollection, nameof(serviceCollection), "IServiceCollection is not dependency injection");
            Check.Argument.IsNotNull(optionsAction, nameof(optionsAction));

            optionsAction.Invoke(new RabbitmqKitOptionsBuilder(serviceCollection));

            return serviceCollection;
        }
    }
}
