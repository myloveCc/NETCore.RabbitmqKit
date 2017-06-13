using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using NETCore.RabbitmqKit.Core;

namespace NETCore.RabbitmqKit.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRabbitmqSend _RabbitmqSendService;

        public HomeController(IRabbitmqSend rabbitmqSend)
        {
            _RabbitmqSendService = rabbitmqSend;
        }


        public IActionResult Index()
        {
            _RabbitmqSendService.Send(channel =>
            {
                channel.QueueDeclare(queue: "asp.netcore.web",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes("Asp.net core rabbitmq");

                var bodyProperites = channel.CreateBasicProperties();
                bodyProperites.Persistent = true;

                channel.BasicPublish(exchange: string.Empty,
                                     routingKey: "asp.netcore.web",
                                     mandatory: false,
                                     basicProperties: bodyProperites,
                                     body: body);
            });

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
