using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR_WCF2;
using KSRL4E7.ServiceReference1;


namespace KSRL4E7
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var client5 = new ServiceReference1.Zadanie5Client();

            Console.WriteLine(client5.ScalNapisy(client5.ScalNapisy("zadanie 7 ", " dziala"), " dobrze"));
            Console.ReadKey();
            

            var client6 = new ServiceReference1.Zadanie6Client(new InstanceContext(new HandlerZad6()));
            client6.Dodaj(2, 3);
            Console.ReadKey();
            ((IDisposable)client5).Dispose();
            ((IDisposable)client6).Dispose();
        }
    }

    public class HandlerZad6 : IZadanie6Callback
    {
        public void Wynik(int wyn)
        {
            Console.WriteLine($"Wynik: {wyn}");
        }
    }
}