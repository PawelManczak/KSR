using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KSRL6E6_client
{
    [ServiceContract]
    public interface IZadanie6
    {
        [OperationContract]
        int Dodaj(int a, int b);
    }
    class Program
    {
        static void Main(string[] args)
        {
            var fac = new ChannelFactory<IZadanie6>(
                new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/router")
            );

            var client = fac.CreateChannel();

            Console.WriteLine(client.Dodaj(21, 38));

            Console.ReadKey();

            ((IDisposable)client).Dispose();

            fac.Close();
        }
    }
}
