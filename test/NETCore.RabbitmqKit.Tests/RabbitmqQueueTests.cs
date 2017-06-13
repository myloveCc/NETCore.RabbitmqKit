using NETCore.RabbitmqKit.Core;
using NETCore.RabbitmqKit.Infrastructure;
using System;
using System.Collections.Generic;
using Xunit;

namespace NETCore.RabbitmqKit.Tests
{
    public class RabbitmqQueueTests
    {
        private readonly IRabbitmqQueue _RabbitmqQueue;

        public RabbitmqQueueTests()
        {
            IRabbitmqProvider provider = new RabbitmqProvider(new RabbitmqKitOptions(new List<string>() { "127.0.0.1" }));

            _RabbitmqQueue = new RabbitmqQueue(provider);
        }

        [Fact]
        public void Test1()
        {
        }
    }
}
