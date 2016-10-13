using System;
using System.ServiceModel;
using System.Threading;

namespace RawXmlMessagesExample.Services
{
    class Program
    {
        static void Main()
        {
            using (var host = new ServiceHost(typeof(ComplexService)))
            {
                ////If you want to add Behavior without Attribute mechanism
                //foreach (ServiceEndpoint endpoint in host.Description.Endpoints)
                //{
                //    endpoint.Behaviors.Add(
                //        new AddXmlToFileDispatcherServiceBehavior(@"C:\Temp\AdditionalLogs\Service"));
                //}

                host.Open();

                Console.WriteLine("Complex service started");

                Thread.Sleep(1000);
            }
        }
    }
}
