using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace L4E7
{

    // z7

    [ServiceContract]
    public interface IZadanie7
    {
        [OperationContract]
        [FaultContract(typeof(Wyjatek7))]
        void RzucWyjatek7(string a, int b);
    }

    [DataContract]
    public class Wyjatek7
    {
        [DataMember] public string Opis { get; set; }
        [DataMember] public string A { get; set; }
        [DataMember] public int B { get; set; }

    }

    public class Zadanie7 : IZadanie7
    {
        public void RzucWyjatek7(string a, int b)
        {
            var exception = new FaultException<Wyjatek7>(new Wyjatek7(),
                new FaultReason("Wyjatek z zadania 7 qwe" + a + b));

            throw exception;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // z7
            var host7 = new ServiceHost(typeof(Zadanie7));
            host7.AddServiceEndpoint(typeof(IZadanie7),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/zad7"
                );
            var b7 = host7.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (b7 == null) b7 = new ServiceMetadataBehavior();
            host7.Description.Behaviors.Add(b7);
            host7.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                "net.pipe://localhost/metadane2");

            var client7 = new ServiceReference1.Zadanie7Client();

            host7.Open();
            Console.ReadKey();
            host7.Close();
            

        }
    }
}
