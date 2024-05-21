using System;
using System.Threading.Tasks;
using MassTransit;
using shared;

namespace Controller
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Kontroler");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("amqps://zyfkaljc:i9rkwiZr1uYcU1abPKWEccn6NpjuDihg@cow.rmq2.cloudamqp.com/zyfkaljc"), h =>
                {
                    h.Username("zyfkaljc");
                    h.Password("i9rkwiZr1uYcU1abPKWEccn6NpjuDihg");
                });
            });

            await busControl.StartAsync();

            try
            {
                while (true)
                {
                    var key = Console.ReadKey().Key;
                    string iv = Guid.NewGuid().ToString();

                    switch (key)
                    {
                        case ConsoleKey.S:
                            await busControl.Publish(new Ustaw { Dziala = "True" });
                            Console.WriteLine("Wysylanie komendy start");
                            break;
                        case ConsoleKey.T:
                            await busControl.Publish(new Ustaw { Dziala = "False" });
                            Console.WriteLine("Wysylanie komendy stop");
                            break;
                    }
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
