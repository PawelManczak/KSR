using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Routing;
using System.Text;
using System.Threading.Tasks;

namespace KSRL6E6_router
{
    class Program
    {
        static void Main(string[] args)
        {
            var routeService1 = "net.pipe://localhost/exercise6-service1";
            var routeService2 = "net.pipe://localhost/exercise6-service2";
            var routeRouter = "net.pipe://localhost/router";

            var host = new ServiceHost(typeof(RoutingService));

            host.AddServiceEndpoint(
                typeof(IRequestReplyRouter),
                new NetNamedPipeBinding(),
                routeRouter
            );

            var routeConfig = new RoutingConfiguration();

            var contract = ContractDescription.GetContract(typeof(IRequestReplyRouter));

            var client1 = new ServiceEndpoint(
                contract,
                new NetNamedPipeBinding(),
                new EndpointAddress(routeService1)
            );

            var client2 = new ServiceEndpoint(
                contract,
                new NetNamedPipeBinding(),
                new EndpointAddress(routeService2)
            );

            var list = new List<ServiceEndpoint>();
            list.Add(client1);
            list.Add(client2);

            routeConfig.FilterTable.Add(new MatchAllMessageFilter(), list);
            host.Description.Behaviors.Add(new RoutingBehavior(routeConfig));

            host.Open();
            Console.ReadKey();
            host.Close();
        }
    }
}
