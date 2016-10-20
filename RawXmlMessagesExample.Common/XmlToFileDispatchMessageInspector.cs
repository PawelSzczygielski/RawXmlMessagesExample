using System;
using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;

namespace RawXmlMessagesExample.Common
{
    internal sealed class XmlToFileDispatchMessageInspector : IDispatchMessageInspector, IClientMessageInspector
    {
        private readonly string _storageFolderPath;

        public XmlToFileDispatchMessageInspector(string storageFolderPath)
        {
            _storageFolderPath = storageFolderPath;
            if (!Directory.Exists(_storageFolderPath))
                Directory.CreateDirectory(_storageFolderPath);
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

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var filePath = GetFilePath(request);
            var body = GetMessageWithEnvelope(ref request);
            AppendToFile(filePath, body);
            return filePath;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            var filePath = correlationState.ToString();
            var body = GetMessageWithEnvelope(ref reply);

            AppendToFile(filePath, $"{Environment.NewLine}***RESPONSE***{Environment.NewLine}");
            AppendToFile(filePath, body);
        }

        private static void AppendToFile(string filePath, string body)
        {
            using (var fileStream = File.AppendText(filePath))
            {
                fileStream.Write(body);
                fileStream.Flush();
            }
        }

        private static string GetMessageWithEnvelope(ref Message request)
        {
            var buffer = request.CreateBufferedCopy(Int32.MaxValue);
            request = buffer.CreateMessage();
            var msg = buffer.CreateMessage();
            var sb =
                new StringBuilder(
                    $"<?xml version=\"1.0\" encoding=\"utf-8\"?>{Environment.NewLine}<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">{Environment.NewLine}"); //Dirty Hack To Add Envelope itself
            using (
                var xw = XmlWriter.Create(sb,
                    new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8, OmitXmlDeclaration = true }))
                msg.WriteBody(xw);

            sb.Append($"{Environment.NewLine}</soap:Envelope>"); //Dirty Hack To Add Envelope itself
            return sb.ToString();
        }

        private string GetFilePath(Message request)
        {
            var operationName = GetOperationName(request);
            var fileName = $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture)}_{operationName}.txt";
            var filePath = Path.Combine(_storageFolderPath, fileName);
            return filePath;
        }

        private static string GetOperationName(Message request)
        {
            var fullActionName = request.Headers.Action;
            var lastSlashIndex = fullActionName.LastIndexOf("/", StringComparison.Ordinal) + 1;
            var operationName = fullActionName.Substring(lastSlashIndex, fullActionName.Length - lastSlashIndex);
            return operationName;
        }
    }
}