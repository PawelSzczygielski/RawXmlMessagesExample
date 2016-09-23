using System;
using System.Collections.Generic;

namespace RawXmlMessagesExample.Client.RemoteComplexService
{
    partial class ComplexRequest
    {
        public ComplexRequest(Guid id, string name, List<DeepObject> subObjects)
        {
            Id = id;
            Name = name;
            SubObjects = subObjects;
        }
    }

    partial class DeepObject
    {
        public DeepObject(Guid id, string name, int quantity, DateTime timeStamp)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            TimeStamp = timeStamp;
        }
    }
}
