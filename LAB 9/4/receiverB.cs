// Program.cs (Abonament B)
using System;
using System.Threading.Tasks;
using MassTransit;
using shared;

namespace AbonamentB
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("abonament B");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("amqps://zyfkaljc:i9rkwiZr1uYcU1abPKWEccn6NpjuDihg@cow.rmq2.cloudamqp.com/zyfkaljc"), h =>
                {
                    h.Username("zyfkaljc");
                    h.Password("i9rkwiZr1uYcU1abPKWEccn6NpjuDihg");
                });

                cfg.ReceiveEndpoint("subscriberB_queue", ep =>
                {
                    ep.Handler<Publ>(context =>
                    {
                        Console.WriteLine($"Odebrano wiadomość: {context.Message.Number}");
                        if (context.Message.Number % 3 == 0)
                        {
                            var response = new OdpB { Kto = "abonament B" };
                            return context.RespondAsync(response);
                        }

                        return Task.CompletedTask;
                    });

                    ep.Handler<ExceptionB>(context =>
                    {
                        Console.WriteLine($"Odpowiedź {context.Message.message} wywołała wyjątek: {context.Message.exceptionmessage}");
                        return Task.CompletedTask;
                    });
                });
            });

            await busControl.StartAsync();
            try
            {
                Console.ReadLine();
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }
}
