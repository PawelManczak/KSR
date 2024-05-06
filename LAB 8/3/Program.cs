using KSRL8Komunikaty;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSRL8E2
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost"), cfg =>
                {
                    cfg.Username("guest");
                    cfg.Password("guest");
                });

                sbc.ReceiveEndpoint("recvqueue", ep => {
                    ep.Handler<Komunikat>(Handle);
                });

            });

            bus.Start();

            Console.WriteLine("Odbiorca wystartował. Oczekiwanie na wiadomości...");
            Console.ReadKey();

            bus.Stop();
        }

        static async Task Handle(ConsumeContext<Komunikat> ctx)
        {
            await Console.Out.WriteLineAsync($"Odebrano: {ctx.Message.Tekst}");
            foreach (var hdr in ctx.Headers.GetAll())
            {
                Console.WriteLine("nagłówek: {0}: {1}", hdr.Key, hdr.Value);
            }
        }
    }
}