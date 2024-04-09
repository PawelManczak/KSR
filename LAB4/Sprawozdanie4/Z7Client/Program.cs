using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Z7Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client7 = new ServiceReference2.Zadanie7Client();
            try
            {
                client7.RzucWyjatek7("188756", 2);
            }
            catch(FaultException<ServiceReference2.Wyjatek7> e)
            {
                Console.WriteLine(e.Reason);
            }
            Console.ReadLine();
        }
    }
}
