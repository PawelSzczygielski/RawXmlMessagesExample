using RawXmlMessagesExample.Client.RemoteComplexService;

namespace RawXmlMessagesExample.Client
{
    class Program
    {
        static void Main()
        {
            using (var client = new ComplexServiceClient("ComplexService_http"))
            {
                var response = client.DoComplexOperation(new ComplexRequest());
            }

        }
    }
}
