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
                host.Open();

                Console.WriteLine("Complex service started");

                while (true)
                    Thread.Sleep(1000);
            }
        }
    }
}
