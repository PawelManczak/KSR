// Program.cs (Wydawca)
using System;
using System.Threading.Tasks;
using MassTransit;
using shared;

namespace Wydawca_zadanie1
{
    internal class Program
    {
        private static bool isRunning = false;
        private static int counter = 1;

        static async Task Main(string[] args)
        {
            Console.WriteLine("wydawca");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("amqps://zyfkaljc:i9rkwiZr1uYcU1abPKWEccn6NpjuDihg@cow.rmq2.cloudamqp.com/zyfkaljc"), h =>
                {
                    h.Username("zyfkaljc");
                    h.Password("i9rkwiZr1uYcU1abPKWEccn6NpjuDihg");
                });

                cfg.ReceiveEndpoint("publisher_queue", ep =>
                {
                    ep.Handler<Ustaw>(context =>
                    {
                        if (context.Message.Dziala == "True")
                        {
                            isRunning = true;
                            Console.WriteLine("Generator uruchomiony");
                        }
                        else if (context.Message.Dziala == "False")
                        {
                            isRunning = false;
                            Console.WriteLine("Generator zatrzymany");
                        }
                        return Task.CompletedTask;
                    });

                    ep.Handler<OdpA>(context =>
                    {
                        Console.WriteLine($"Odpowiedź od abonament A: {context.Message.Kto}");
                        return Task.CompletedTask;
                    });

                    ep.Handler<OdpB>(context =>
                    {
                        Console.WriteLine($"Odpowiedź od abonament B: {context.Message.Kto}");
                        return Task.CompletedTask;
                    });
                });
            });

            await busControl.StartAsync();

            try
            {
                while (true)
                {
                    if (isRunning)
                    {
                        var message = new Publ { Number = counter++ };
                        await busControl.Publish(message);
                        Console.WriteLine($"Opublikowano wiadomość: {message.Number}");
                    }

                    await Task.Delay(1000);
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
