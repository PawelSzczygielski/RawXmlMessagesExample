using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RawXmlMessagesExample.Services
{
    [ServiceContract(SessionMode = SessionMode.Allowed, Namespace = "http://PawelSzczygielski/v1_0/RawXmlMessageExampleService")]
    public interface IComplexService
    {
        [OperationContract]
        ComplexResponse DoComplexOperation(ComplexRequest obj);
    }

    [ServiceBehavior]
    public class ComplexService : IComplexService
    {
        public ComplexResponse DoComplexOperation(ComplexRequest obj)
        {
            if(obj == null)
                throw new WebFaultException<string>("Invalid message", HttpStatusCode.BadRequest);

            return new ComplexResponse(Guid.NewGuid(), new byte[] {1, 1, 2});
        }
    }
}