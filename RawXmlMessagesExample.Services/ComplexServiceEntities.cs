using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RawXmlMessagesExample.Services
{
    [DataContract]
    public class ComplexResponse
    {
        public ComplexResponse(Guid id, byte[] blob)
        {
            Id = id;
            Blob = blob;
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public byte[] Blob { get; set; }
        
    }

    [DataContract]
    public class ComplexRequest
    {
        public ComplexRequest(Guid id, string name, List<DeepObject> subObjects)
        {
            Id = id;
            Name = name;
            SubObjects = subObjects;
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<DeepObject> SubObjects { get; set; }
    }

    [DataContract]
    public class DeepObject
    {
        public DeepObject(Guid id, int quantity, string name, DateTime timeStamp)
        {
            Id = id;
            Quantity = quantity;
            Name = name;
            TimeStamp = timeStamp;
        }

        [DataMember]
        public Guid Id { get;  set; }
        [DataMember]
        public int Quantity { get;  set; }
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public DateTime TimeStamp { get;  set; }
    }
}