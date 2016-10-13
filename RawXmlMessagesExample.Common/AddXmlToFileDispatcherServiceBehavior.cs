using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace RawXmlMessagesExample.Common
{
    public sealed class AddXmlToFileDispatcherServiceBehavior : Attribute, IServiceBehavior, IEndpointBehavior
    {
        private readonly string _storageFolderPath;

        public AddXmlToFileDispatcherServiceBehavior(string storageFolderPath)
        {
            _storageFolderPath = storageFolderPath;
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var chDisp = (ChannelDispatcher)channelDispatcherBase;
                foreach (EndpointDispatcher epDisp in chDisp.Endpoints)
                    epDisp.DispatchRuntime.MessageInspectors.Add(new XmlToFileDispatchMessageInspector(_storageFolderPath));
            }
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            var inspector = new XmlToFileDispatchMessageInspector(_storageFolderPath);
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {

        }
    }
}