using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NETCore.RabbitmqKit.Core;
using NETCore.RabbitmqKit.Infrastructure;

namespace NETCore.RabbitmqKit.Tests
{
    public class RabbitmqExchangeTests
    {
        private readonly IRabbitmqExchange _RabbitmqExchange;

        public RabbitmqExchangeTests()
        {
            IRabbitmqProvider provider = new RabbitmqProvider(new RabbitmqKitOptions(new List<string>() { "127.0.0.1" }));

            _RabbitmqExchange = new RabbitmqExchange(provider);
        }

        [Fact]
        public void Test1()
        {
        }
    }
}
