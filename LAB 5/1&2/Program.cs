using KSR_WCF2;
using KSRL4E1.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KSRL4E1
{
    public class Handler : IZadanie2Callback
    {
        public IAsyncResult BeginZadanie(string zadanie, int pkt, bool zaliczone, AsyncCallback callback, object asyncState)
        {
            Console.Write("begin zadanie");
            return null;
        }

        public void EndZadanie(IAsyncResult result)
        {
            Console.Write("end zadanie:" + result.AsyncState);
            
        }

        public void Zadanie(string nazwaPodzadania, int liczbaPunktow, bool czyZaliczone)
        {
            Console.WriteLine($"nazwaPodzadania: {nazwaPodzadania}, liczbaPunktow: {liczbaPunktow}, czyZaliczone: {czyZaliczone}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // 1

            var klient1 = new ServiceReference1.Zadanie1Client();
            IAsyncResult wynik = klient1.BeginDlugieObliczenia(null, null);

            for (int x = 0; x <= 20; x++)
            {
                Console.WriteLine(klient1.Szybciej(x, 3 * x * x - 2 * x));
            }

            Console.WriteLine(klient1.EndDlugieObliczenia(wynik));
            Console.WriteLine("ex 1 done");
            Console.ReadKey();

           ((IDisposable)klient1).Dispose();

            // 2
            InstanceContext instanceContext = new InstanceContext(new Handler());
            var klient2 = new ServiceReference1.Zadanie2Client(instanceContext);

            klient2.PodajZadania();
            Console.ReadKey();

            ((IDisposable)klient2).Dispose();

        }
    }

}
