
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace Lab4E1
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

    public class Zadanie7: IZadanie7
    {
        public void RzucWyjatek7(string a, int b)
        {
            var exception = new FaultException<Wyjatek7>(new Wyjatek7(),
                new FaultReason("Wyjatek z zadania 7 qwe" + a + b));

            throw exception;
        }
    }
    public class Class1
    {
        static void Main(string[] args)
        {
            // z1
            var fact = new ChannelFactory<KSR_WCF1.IZadanie1>(new NetNamedPipeBinding(),
            new EndpointAddress("net.pipe://localhost/ksr-wcf1-test"));
            var client = fact.CreateChannel();
            Console.WriteLine(client.Test("188756 1 zadanie dziala!"));

            // z5

            try
            {
                client.RzucWyjatek(true);

            }
            catch(FaultException<KSR_WCF1.Wyjatek> e)
            {
                Console.WriteLine(e.Detail.opis);
                client.OtoMagia(e.Detail.magia);
            }
            // z7
            var host7 = new ServiceHost(typeof(Zadanie7));
            host7.AddServiceEndpoint(typeof(IZadanie7),
                new NetNamedPipeBinding(),
                "net.pipe://localhost/ksr-wcf1-zad7"
                );
            var b7 = host7.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (b7 == null) b7 = new ServiceMetadataBehavior();
            host7.Description.Behaviors.Add(b7);
            host7.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                "net.pipe://localhost/metadane2");

            ((IDisposable)client).Dispose();
            fact.Close();
            Console.ReadKey();

            


        }
    }
}
