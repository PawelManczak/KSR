
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                channel.ExchangeDeclare("abc.def", ExchangeType.Fanout);
                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: "abc.def", routingKey: "");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"Abonent 1 - Odebrano wiadomość: {message}");
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                Console.ReadKey();
            }
        }

    }
}
