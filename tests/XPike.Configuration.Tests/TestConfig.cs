using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XPike.Configuration.Tests
{
    [Serializable]
    [DataContract]
    public class TestConfig
    {
        [DataMember]
        public Dictionary<string, Dictionary<string, string>> Groups { get; set; }
    }
}