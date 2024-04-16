using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KSR_WCF2;
using System.ServiceModel;


namespace Zadanie3_serwis
{

    public class Zadanie3 : IZadanie3
    {
        public void TestujZwrotny()
        {
            for (int x = 0; x <= 30; x++)
            {
                OperationContext.Current.GetCallbackChannel<IZadanie3Zwrotny>().WolanieZwrotne(x, x * x * x - x * x);
            }
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Zadanie4 : IZadanie4
    {
        private int licznik;

        public void Ustaw(int liczba)
        {
            licznik = liczba;
        }

        public int Dodaj(int liczba)
        {
            licznik += liczba;
            return licznik;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 3
            var serwis = new ServiceHost(typeof(Zadanie3));

            serwis.AddServiceEndpoint(
                typeof(IZadanie3),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf2-zad3");

            // 4
            var serwis4 = new ServiceHost(typeof(Zadanie4));

            serwis4.AddServiceEndpoint(
                typeof(IZadanie4),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf2-zad4");

            serwis.Open();
            serwis4.Open();

            Console.ReadKey();

            serwis.Close();
            serwis4.Close();
        }
    }
}
