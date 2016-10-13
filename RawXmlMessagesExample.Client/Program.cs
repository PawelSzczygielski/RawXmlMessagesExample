using System;
using System.Collections.Generic;
using RawXmlMessagesExample.Client.RemoteComplexService;
using RawXmlMessagesExample.Common;

namespace RawXmlMessagesExample.Client
{
    class Program
    {
        static void Main()
        {
            using (var client = new ComplexServiceClient("ComplexService_http"))
            {
                client.Endpoint.Behaviors.Add(new AddXmlToFileDispatcherServiceBehavior(@"C:\Temp\AdditionalLogs\Client"));

                var response = client.DoComplexOperation(new ComplexRequest(Guid.NewGuid(), "DoSomething", new List<DeepObject>
                {
                    new DeepObject(Guid.NewGuid(), "First deep object", Int32.MaxValue, DateTime.UtcNow)
                }));
            }

        }
    }



}
