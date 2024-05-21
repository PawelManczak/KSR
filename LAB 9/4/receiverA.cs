// Program.cs (Abonament A)
using System;
using System.Threading.Tasks;
using MassTransit;
using shared;

namespace AbonamentA
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("abonament A");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("amqps://zyfkaljc:i9rkwiZr1uYcU1abPKWEccn6NpjuDihg@cow.rmq2.cloudamqp.com/zyfkaljc"), h =>
                {
                    h.Username("zyfkaljc");
                    h.Password("i9rkwiZr1uYcU1abPKWEccn6NpjuDihg");
                });

                cfg.ReceiveEndpoint("subscriberA_queue", ep =>
                {
                    ep.Handler<Publ>(context =>
                    {
                        Console.WriteLine($"Otrzymano wiadomość: {context.Message.Number}");
                        if (context.Message.Number % 2 == 0)
                        {
                            var response = new OdpA { Kto = "abonament A" };
                            return context.RespondAsync(response);
                        }

                        return Task.CompletedTask;
                    });

                    ep.Handler<ExceptionA>(context =>
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
