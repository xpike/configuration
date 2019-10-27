using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace XPike.Configuration.Microsoft
{
    class x
        : global::Microsoft.Extensions.Configuration.IConfigurationProvider
    {
        public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath) => throw new NotImplementedException();
        public IChangeToken GetReloadToken() => throw new NotImplementedException();
        public void Load() => throw new NotImplementedException();
        public void Set(string key, string value) => throw new NotImplementedException();
        public bool TryGet(string key, out string value) => throw new NotImplementedException();
    }

    class y
        : global::Microsoft.Extensions.Configuration.IConfigurationSource
    {
        public global::Microsoft.Extensions.Configuration.IConfigurationProvider Build(IConfigurationBuilder builder) => throw new NotImplementedException();
    }
}
