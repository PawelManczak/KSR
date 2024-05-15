using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace KSRL9Controller
{
    

    public class Controller
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

                cfg.Message<Ustaw>(x => x.SetEntityName("control_queue"));
            });

            await busControl.StartAsync();
            try
            {
                while (true)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.S)
                    {
                        await busControl.Publish(new Ustaw { Dziala = true });
                        Console.WriteLine("Wysłano polecenie: Start");
                    }
                    else if (key == ConsoleKey.T)
                    {
                        await busControl.Publish(new Ustaw { Dziala = false });
                        Console.WriteLine("Wysłano polecenie: Stop");
                    }
                }
            }
            finally
            {
                await busControl.StopAsync();
            }
        }
    }

    public class Ustaw
    {
        public bool Dziala { get; set; }
    }

}
