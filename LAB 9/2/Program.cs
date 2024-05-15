using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using System;
using System.Threading.Tasks;
using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KSRL9E1
{
   

    public class Publisher
    {
        private static bool isRunning = false;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Wydawca");

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("amqps://zyfkaljc:i9rkwiZr1uYcU1abPKWEccn6NpjuDihg@cow.rmq2.cloudamqp.com/zyfkaljc"), h =>
                {
                    h.Username("zyfkaljc");
                    h.Password("i9rkwiZr1uYcU1abPKWEccn6NpjuDihg");
                });

                cfg.ReceiveEndpoint("control_queue", ep =>
                {
                    ep.Handler<Ustaw>(context =>
                    {
                        isRunning = context.Message.Dziala;
                        Console.WriteLine($"Otrzymano polecenie: Dziala = {isRunning}");
                        return Task.CompletedTask;
                    });
                });
            });

            await busControl.StartAsync();
            try
            {
                int counter = 1;
                while (true)
                {
                    if (isRunning)
                    {
                        await busControl.Publish(new Publ { Number = counter });
                        Console.WriteLine($"Opublikowano wiadomość: {counter}");
                        counter++;
                    }
                    Thread.Sleep(1000);
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }

    public class Publ
    {
        public int Number { get; set; }
    }

    public class Ustaw
    {
        public bool Dziala { get; set; }
    }

}
