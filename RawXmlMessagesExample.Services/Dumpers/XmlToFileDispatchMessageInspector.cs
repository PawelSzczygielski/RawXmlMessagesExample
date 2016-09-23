using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace RawXmlMessagesExample.Services
{
    public sealed class XmlToFileDispatchMessageInspector : XmlToFileBehaviorBase, IDispatchMessageInspector
    {
        public XmlToFileDispatchMessageInspector(string storageFolderPath) : base(storageFolderPath)
        {
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var filePath = GetFilePath(request);
            var body = GetMessageWithEnvelope(ref request);
            AppendToFile(filePath, body);
            return filePath;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var filePath = correlationState.ToString();
            var body = GetMessageWithEnvelope(ref reply);

            AppendToFile(filePath, $"{Environment.NewLine}***RESPONSE***{Environment.NewLine}");
            AppendToFile(filePath, body);
        }
    }
}