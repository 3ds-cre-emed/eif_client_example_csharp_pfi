using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpEIF.AppObjects
{
    // AgentMessage myDeserializedClass = JsonConvert.DeserializeObject<AgentMessage>(myJsonResponse);
    public class Data
    {
        public string eventClass { get; set; }
        public string eventType { get; set; }
        public long eventTime { get; set; }
        public string serviceName { get; set; }
        public string user { get; set; }
        public string authorization { get; set; }
        public Subject subject { get; set; }
        public string predicate { get; set; }
        public Object @object { get; set; }
    }

    public class Metadata
    {
        public string serviceId { get; set; }
        public string geoId { get; set; }
        public string tenantId { get; set; }
    }

    public class Object
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string Value { get; set; }
    }

    public class AgentMessage
    {
        public string specversion { get; set; }
        public string source { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public DateTime time { get; set; }
        public long timestamp { get; set; }
        public Metadata metadata { get; set; }
        public string contenttype { get; set; }
        public Data data { get; set; }
    }

    public class Subject
    {
        public string source { get; set; }
        public string type { get; set; }
        public string identifier { get; set; }
        public string relativePath { get; set; }
    }


}
