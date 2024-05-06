using KSRL8Komunikaty;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KSRL8OdbiorcaB
{
    public class OdbiorcaB : IConsumer<Komunikat>
    {
        private int _licznikWiadomosci;
        private List<string> _odebraneWiadomosci;

        public OdbiorcaB()
        {
            _licznikWiadomosci = 0;
            _odebraneWiadomosci = new List<string>();
        }

        public async Task Consume(ConsumeContext<Komunikat> ctx)
        {
            Interlocked.Increment(ref _licznikWiadomosci);
            _odebraneWiadomosci.Add(ctx.Message.Tekst);
        }

        public void WyswietlWiadomosci()
        {
            Console.WriteLine("Odebrane wiadomości:");
            foreach (var wiadomosc in _odebraneWiadomosci)
            {
                Console.WriteLine(wiadomosc);
            }
            Console.WriteLine($"Liczba odebranych wiadomości: {_licznikWiadomosci}");
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var odbiorcaB = new OdbiorcaB();

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost"), cfg =>
                {
                    cfg.Username("guest");
                    cfg.Password("guest");
                });

                sbc.ReceiveEndpoint("recvqueue", ep => {
                    ep.Consumer(() => odbiorcaB);
                });

            });

            bus.Start();

            Console.WriteLine("Odbiorca B wystartował. Oczekiwanie na wiadomości...");
            Console.CancelKeyPress += (sender, e) =>
            {
                odbiorcaB.WyswietlWiadomosci();
                bus.Stop();
            };

            await Task.Delay(-1); // Czekaj na zakończenie pracy konsumera
        }
    }
}
