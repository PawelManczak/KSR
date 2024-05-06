using KSRL8Komunikaty;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSRL8E1
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            bus.Start();

            Console.WriteLine("Wydawca wystartował");

            Console.WriteLine("Naciśnij dowolny klawisz, aby wysłać komunikat...");
            Console.ReadKey();

            for (int i = 0; i <10; i++)
            {
                bus.Publish(new Komunikat() { Tekst = $"Treść komunikatu od wydawcy {i}" });
            }
            

            Console.WriteLine("Wysłano komunikat");

            Console.ReadKey();

            bus.Stop();
        }
    }
}
