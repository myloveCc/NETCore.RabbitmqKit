using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NETCore.RabbitmqKit;
using NETCore.RabbitmqKit.Core;
using NETCore.RabbitmqKit.Infrastructure;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NETCore.RabbitmqKit.Tests
{
    public class RabbitmqServiceTests
    {
        private readonly IRabbitmqSend _RabbitmqSendService;
        private readonly IRabbitmqReceive _RabbitmqReceiveService;

        public RabbitmqServiceTests()
        {
            IRabbitmqProvider provider = new RabbitmqProvider(new RabbitmqKitOptions(hostNames: new List<string>() { "127.0.0.1" }, userName: "Lvcc", password: "123456"));

            _RabbitmqSendService = new RabbitmqSend(provider);
            _RabbitmqReceiveService = new RabbitmqReceive(provider);
        }

        [Fact(DisplayName = "UnDurable_Message_Test")]
        public void Send_Basic_Message_Test()
        {
            var sendMessage = "Hello world";
            //send message
            _RabbitmqSendService.Send(channel =>
            {
                channel.QueueDeclare(queue: "test_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(sendMessage);


                //note:publish's routingKey is queuename
                channel.BasicPublish(exchange: "",
                               routingKey: "test_queue",
                               basicProperties: null,
                               body: body);
            });
        }

        [Fact(DisplayName = "Durable_Message_Test")]
        public void Send_Durable_Message_Test()
        {
            var sendMessage = "Durable Hello world";

            _RabbitmqSendService.Send(channel =>
            {

                channel.QueueDeclare(queue: "test_queue_durable",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //set message save to disk
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var body = Encoding.UTF8.GetBytes(sendMessage);


                //note:publish's routingKey is queuename
                channel.BasicPublish(exchange: "",
                               routingKey: "test_queue_durable",
                               basicProperties: properties,
                               body: body);
            });
        }

        [Fact(DisplayName = "Exchange_Fanout_Test")]
        public void Send_Exchange_Fanout_Message_Test()
        {
            var sendMessage = "Fanout exchange message";

            _RabbitmqSendService.Send(channel =>
            {

                //fanout exchange
                channel.ExchangeDeclare("exchange_fanout", ExchangeType.Fanout, true, false, null);

                //queue
                channel.QueueDeclare("fanout_queue_1", true, false, false, null);

                channel.QueueDeclare("fanout_queue_2", true, false, false, null);

                channel.QueueDeclare("fanout_queue_3", true, false, false, null);

                //queue bind
                channel.QueueBind(queue: "fanout_queue_1",
                                  exchange: "exchange_fanout",
                                  routingKey: "");

                channel.QueueBind(queue: "fanout_queue_2",
                                  exchange: "exchange_fanout",
                                  routingKey: "");

                channel.QueueBind(queue: "fanout_queue_3",
                           exchange: "exchange_fanout",
                           routingKey: "");

                var body = Encoding.UTF8.GetBytes(sendMessage);

                //mesage save to disk
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                //send message
                channel.BasicPublish(exchange: "exchange_fanout",
                          routingKey: "",
                          basicProperties: properties,
                          body: body);

            });
        }

        [Theory(DisplayName = "Exchange_Direct_Test")]
        [InlineData("direct_read")]
        [InlineData("direct_delete")]
        [InlineData("direct_write")]
        public void Send_Exchange_Direct_Message_Test(string routingKey)
        {
            var sendMessage = "Direct exchange message";

            _RabbitmqSendService.Send(channel =>
            {

                //direct exchange
                channel.ExchangeDeclare("exchange_direct", ExchangeType.Direct, true, false, null);

                //queue
                channel.QueueDeclare("direct_queue_1", true, false, false, null);

                channel.QueueDeclare("direct_queue_2", true, false, false, null);

                channel.QueueDeclare("direct_queue_3", true, false, false, null);

                //queue bind
                channel.QueueBind(queue: "direct_queue_1",
                                  exchange: "exchange_direct",
                                  routingKey: "direct_read");

                channel.QueueBind(queue: "direct_queue_2",
                                  exchange: "exchange_direct",
                                  routingKey: "direct_delete");

                channel.QueueBind(queue: "direct_queue_3",
                           exchange: "exchange_direct",
                           routingKey: "direct_write");

                var body = Encoding.UTF8.GetBytes($"{sendMessage},Type:{routingKey}");

                //mesage save to disk
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                //send message
                channel.BasicPublish(exchange: "exchange_direct",
                          routingKey: routingKey,
                          basicProperties: properties,
                          body: body);

            });
        }

        [Theory(DisplayName = "Exchange_Topic_Test")]
        [InlineData("topic.user.lvcc.create")]
        [InlineData("topic.user.lvcc.delete")]
        [InlineData("topic.user.lvcc.read")]
        public void Send_Exchange_Topic_Message_Test(string routingKey)
        {
            var sendMessage = "Topic exchange message";

            _RabbitmqSendService.Send(channel =>
            {

                //topic exchange
                channel.ExchangeDeclare("exchange_topic", ExchangeType.Topic, true, false, null);

                //queue
                channel.QueueDeclare("topic_queue_1", true, false, false, null);

                channel.QueueDeclare("topic_queue_2", true, false, false, null);

                channel.QueueDeclare("topic_queue_3", true, false, false, null);

                //queue bind
                channel.QueueBind(queue: "topic_queue_1",
                                  exchange: "exchange_topic",
                                  routingKey: "topic.user.lvcc.#");

                channel.QueueBind(queue: "topic_queue_2",
                                  exchange: "exchange_topic",
                                  routingKey: "topic.user.#");

                channel.QueueBind(queue: "topic_queue_3",
                           exchange: "exchange_topic",
                           routingKey: "topic.user.*.read");


                var body = Encoding.UTF8.GetBytes($"{sendMessage},Type:{routingKey}");

                //mesage save to disk
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                //send message
                channel.BasicPublish(exchange: "exchange_topic",
                          routingKey: routingKey,
                          basicProperties: properties,
                          body: body);

            });
        }

        [Theory(DisplayName = "Exchange_Header_Test")]
        [InlineData("log")]
        [InlineData("report")]
        public void Send_Exchange_Header_Message_Test(string type)
        {
            var sendMessage = "Header exchange message";

            _RabbitmqSendService.Send(channel =>
            {
                //header exchange
                channel.ExchangeDeclare("exchange_header", ExchangeType.Topic, true, false, null);

                //queue
                channel.QueueDeclare("header_queue_1", true, false, false, null);

                channel.QueueDeclare("header_queue_2", true, false, false, null);

                channel.QueueDeclare("header_queue_3", true, false, false, null);

                //queue bind
                Dictionary<string, object> aHeader = new Dictionary<string, object>();
                aHeader.Add("format", "pdf");
                aHeader.Add("type", "report");
                aHeader.Add("x-match", "all");
                channel.QueueBind(queue: "header_queue_1",
                                  exchange: "exchange_header",
                                  routingKey: string.Empty,
                                  arguments: aHeader);

                Dictionary<string, object> bHeader = new Dictionary<string, object>();
                bHeader.Add("format", "pdf");
                bHeader.Add("type", "log");
                //Note:x-match=any
                bHeader.Add("x-match", "any");
                channel.QueueBind(queue: "header_queue_2",
                                  exchange: "exchange_header",
                                  routingKey: string.Empty,
                                  arguments: bHeader);

                Dictionary<string, object> cHeader = new Dictionary<string, object>();
                cHeader.Add("format", "zip");
                cHeader.Add("type", "report");
                //Note:x-match=all
                cHeader.Add("x-match", "all");
                channel.QueueBind(queue: "header_queue_3",
                           exchange: "exchange_header",
                           routingKey: string.Empty,
                           arguments: cHeader);


                var body = Encoding.UTF8.GetBytes($"{sendMessage},Type:{type}");

                //mesage save to disk
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                Dictionary<string, object> mHeader = new Dictionary<string, object>();
                mHeader.Add("type", type);

                //set header
                properties.Headers = mHeader;

                //send message
                channel.BasicPublish(exchange: "exchange_header",
                          routingKey: string.Empty,
                          basicProperties: properties,
                          body: body);

            });
        }
    }
}
