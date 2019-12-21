using System.Collections.Generic;

namespace Example.Library
{
    public class AnotherConfig
    {
        public string Name { get; set; }

        public Dictionary<string, Dictionary<string, string>> Groups { get; set; }
    }
}