using KSRL8Komunikaty;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSRL8E1
{
    class Komunikat : IKomunikat
    {
        public string Tekst { get; set; }
    }

    class Komunikat2 : IDrugiTypKomunikatu
    {
        public string TekstDrugiTyp { get; set; }
    }
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

            Console.WriteLine("Naciśnij dowolny klawisz, aby wysłać komunikaty...");
            Console.ReadKey();

            for (int i = 0; i < 10; i++)
            {
                bus.Publish<IKomunikat>(new Komunikat() { Tekst = $"Treść komunikatu od wydawcy {i}" });
                Console.WriteLine($"wysłano: Treść komunikatu od wydawcy {i}");
                bus.Publish<IDrugiTypKomunikatu>(new Komunikat2() { TekstDrugiTyp = $"Treść drugiego typu komunikatu {i}" });
                Console.WriteLine($"wysłano: Treść drugiego typu komunikatu {i}");
            }  

            Console.WriteLine("Wysłano komunikaty");

            Console.ReadKey();

            bus.Stop();
        }
    }
}
