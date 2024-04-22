using System;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;


namespace KSRL6E2
{
    [ServiceContract]
    public interface IZadanie1
    {
        [OperationContract]
        string ScalNapisy(string a, string b);

    }
    class Program
    {
        static void Main(string[] args)
        {
            //Zad2
            var discoveryClient = new DiscoveryClient(
                new UdpDiscoveryEndpoint("soap.udp://localhost:30703"));

            var lst = discoveryClient.Find(new FindCriteria(typeof(IZadanie1))).Endpoints;

            discoveryClient.Close();

            if (lst.Count > 0)
            {
                var addr = lst[0].Address;
                var proxy = ChannelFactory<IZadanie1>
                    .CreateChannel(new NetNamedPipeBinding(), addr);
                Console.WriteLine(proxy.ScalNapisy("exercise 2", " is working 188756"));
                Console.ReadKey();
                ((IDisposable)proxy).Dispose();

                
            }
        }
    }
}