using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Lab4E2
{
    [ServiceContract] 
    public interface IZadanie2
    {
        [OperationContract] string Test(string arg);
    }

    public class Zadanie2: IZadanie2
    {
        public string Test(string arg)
        {
            return $"u're testing with arg: {arg}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(Zadanie2));
            host.AddServiceEndpoint(typeof(IZadanie2),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf1-zad2");

            

            // ex 3
            var b = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (b == null) b = new ServiceMetadataBehavior();
            host.Description.Behaviors.Add(b);

            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
            MetadataExchangeBindings.CreateMexNamedPipeBinding(),
            "net.pipe://localhost/metadane");

            // ex 4
            host.AddServiceEndpoint(typeof(IZadanie2),
                new NetTcpBinding(),
               "net.tcp://127.0.0.1:55765");




            host.Open();
            Console.ReadKey();
            host.Close();
            Console.ReadLine();
            
        }
    }
}
