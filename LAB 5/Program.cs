using KSR_WCF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KSRL4E1
{
    public class Handler : IZadanie2Enum
    {
        public void Zadanie(string nazwaPodzadania, int liczbaPunktow, bool czyZaliczone)
        {
            Console.WriteLine($"Podzadanie: {nazwaPodzadania}, Punkty: {liczbaPunktow}, Zaliczone: {czyZaliczone}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //zadanie1
            var klient1 = new ServiceReference1.Zadanie1Client();
            IAsyncResult wynik = klient1.BeginDlugieObliczenia(null, null);

            for (int x = 0; x <= 20; x++)
            {
                Console.WriteLine(klient1.Szybciej(x, 3 * x * x - 2 * x));
            }
            Console.WriteLine(klient1.EndDlugieObliczenia(wynik));
            Console.WriteLine("Zadanie 1  - koniec");
            Console.ReadKey();

            ((IDisposable)klient1).Dispose();

            // zadanie 2
            InstanceContext instanceContext = new InstanceContext(new Handler());
            var klient2 = new ServiceReference1.Zadanie2Client(instanceContext);

            klient2.PodajZadania();
            Console.ReadKey();

            ((IDisposable)klient2).Dispose();

        }
    }

}
