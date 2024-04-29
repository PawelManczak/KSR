using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSRl7E1
{

    class Program
    {
        static void Main(string[] args)
        {

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
                channel.QueueDeclare("reply_queue", false, false, false, null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"Nadawca - Odebrano odpowiedź: {message}");
                };

                channel.BasicConsume(queue: "reply_queue", autoAck: true, consumer: consumer);

                for (int i = 1; i <= 10; i++)
                {
                    // ex 3
                    var headers = new Dictionary<string, object>
                    {
                        {"Header1", "188756"},
                        {"Header2", i}
                    };
                    var properties = channel.CreateBasicProperties();
                    properties.Headers = headers;

                    string message = $"Wiadomość {i}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "message_queue", basicProperties: properties, body: body);
                    Console.WriteLine($"Wysłano: {message}");
                   // System.Threading.Thread.Sleep(1000);
                }
            }

            Console.ReadKey();

        }
    }
}

