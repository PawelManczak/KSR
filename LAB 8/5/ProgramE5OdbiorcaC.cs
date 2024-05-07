using KSRL8Komunikaty;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSRL8OdbiorcaC
{
    class OdbiorcaC : IConsumer<IDrugiTypKomunikatu>
    {
        public Task Consume(ConsumeContext<IDrugiTypKomunikatu> ctx)
        {
            return Console.Out.WriteLineAsync($"Odebrano wiadomość drugiego typu: {ctx.Message.TekstDrugiTyp}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var odbiorcaC = new OdbiorcaC();

            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(new Uri("rabbitmq://localhost"), cfg =>
                {
                    cfg.Username("guest");
                    cfg.Password("guest");
                });

                sbc.ReceiveEndpoint("recvqueue", ep => {
                    ep.Consumer(() => odbiorcaC);
                });

            });

            bus.Start();

            Console.WriteLine("Odbiorca C wystartował. Oczekiwanie na wiadomości drugiego typu...");
            Console.ReadKey();

            bus.Stop();
        }
    }
}
