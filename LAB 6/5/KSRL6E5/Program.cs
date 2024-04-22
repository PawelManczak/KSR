using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KSRL6E5
{
    class Program
    {
        static void Main(string[] args)
        {
            var fact = new ChannelFactory<IZadanie3>(
                new WebHttpBinding(),
                new EndpointAddress("http://localhost:30703/Service1.svc/zadanie3")
            );

            fact.Endpoint.Behaviors.Add(new WebHttpBehavior());

            var client = fact.CreateChannel();

            Console.WriteLine(client.Dodaj("21", "38"));

            ((IDisposable)client).Dispose();
            fact.Close();
            Console.ReadKey();
        }
    }


    [ServiceContract]
    public interface IZadanie3
    {
        [OperationContract, WebGet(UriTemplate = "index.html"), XmlSerializerFormat]
        XmlDocument Serwuj();

        [OperationContract, WebInvoke(UriTemplate = "Dodaj/{a}/{b}")]
        int Dodaj(string a, string b);

        [OperationContract, WebGet(UriTemplate = "scripts.js")]
        Stream GetStream();
    }
}
