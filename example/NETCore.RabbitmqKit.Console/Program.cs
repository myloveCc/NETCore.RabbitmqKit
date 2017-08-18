using Microsoft.Extensions.DependencyInjection;
using NETCore.RabbitmqKit.Extensions;
using NETCore.RabbitmqKit.Infrastructure;
using NETCore.RabbitmqKit.Core;
using System;
using System.Collections.Generic;
using RabbitMQ.Client.Events;
using System.Text;
using RabbitMQ.Client;
using StructureMap;

namespace NETCore.RabbitmqKit.Cons
{
    class Program
    {

        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddRabbitmqKit(options => {
                options.UseRabbitMQ(new RabbitmqKitOptions(hostNames: new List<string>() { "127.0.0.1" },
                                                           userName: "Lvcc",
                                                           password: "123456"));
            });

            // add StructureMap
            var container = new Container();
            container.Configure(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Program));
                    _.WithDefaultConventions();
                });
                // Populate the container using the service collection
                config.Populate(services);
            });

            var serviceProvider = container.GetInstance<IServiceProvider>();

            Console.WriteLine("Hello RabbitmqKit!");

            IRabbitmqReceive receiveService = serviceProvider.GetService<IRabbitmqReceive>();

            receiveService.Receive(channel => {

                //receive message
                channel.QueueDeclare(queue: "asp.netcore.web",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };

                channel.BasicConsume(queue: "asp.netcore.web",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");

            });
        }
    }
}