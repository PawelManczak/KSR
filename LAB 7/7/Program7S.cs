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
                channel.ExchangeDeclare("abc.def", ExchangeType.Fanout);
                channel.ExchangeDeclare("abc.xyz", ExchangeType.Fanout);

                for (int i = 1; i <= 10; i++)
                {
                    string message = $"Wiadomość {i}";
                    var body = Encoding.UTF8.GetBytes(message);

                    if (i % 2 == 0)
                    {
                        channel.BasicPublish(exchange: "abc.def", routingKey: "", basicProperties: null, body: body);
                        Console.WriteLine($"Wysłano na kanał abc.def: {message}");
                    }
                    else
                    {
                        channel.BasicPublish(exchange: "abc.xyz", routingKey: "", basicProperties: null, body: body);
                        Console.WriteLine($"Wysłano na kanał abc.xyz: {message}");
                    }
                }
            }

            Console.ReadKey();

        }
    }
}

