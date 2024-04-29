
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSRL7E2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Odbiorca:");

            var factory = new ConnectionFactory()
            {
                UserName = "zyfkaljc",
                Password = "i9rkwiZr1uYcU1abPKWEccn6NpjuDihg",
                HostName = "cow.rmq2.cloudamqp.com",
                VirtualHost = "zyfkaljc"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("message_queue", false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Odebrano: {message}");
                };

                channel.BasicConsume(queue: "message_queue", autoAck: true, consumer: consumer);

                Console.WriteLine("click to stop");
                Console.ReadKey();
            }
        }

    }
}
