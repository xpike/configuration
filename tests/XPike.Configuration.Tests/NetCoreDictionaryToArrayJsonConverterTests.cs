using System;
using System.Diagnostics;
using Newtonsoft.Json;
using XPike.Configuration.Microsoft;
using Xunit;
using Xunit.Abstractions;

namespace XPike.Configuration.Tests
{
    public class NetCoreDictionaryToArrayJsonConverterTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public NetCoreDictionaryToArrayJsonConverterTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private const string _RAW_JSON = "{\"MyArray\": {\"0\": 1, \"1\": 2, \"2\": 3}}";
        private const string _RAW_JSON_2 = "{\"MyArray\": [1, 2, 3]}";

        private const string _RAW_JSON_N = "{\"MyArray\": {\"0\": 1, \"1\": null, \"2\": 3}}";
        private const string _RAW_JSON_N2 = "{\"MyArray\": [1, 3]}";

        private const string _RAW_JSON_M = "{\"MyArray\": {\"0\": {\"Name\": \"Item 1\", \"Value\": 1}, \"1\": {\"Name\": \"Item 2\", \"Value\": 2}, \"2\": {\"Name\": \"Item 3\", \"Value\": 3}}}";
        private const string _RAW_JSON_M2 = "{\"MyArray\": [{\"Name\": \"Item 1\", \"Value\": 1}, {\"Name\": \"Item 2\", \"Value\": 2}, {\"Name\": \"Item 3\", \"Value\": 3}]}";

        public class MyClass
        {
            public int[] MyArray { get; set; }
        }

        public class MyClass3
        {
            public MyClass2[] MyArray { get; set; }
        }

        public class MyClass2
        {
            public string Name { get; set; }
            public int Value { get; set; }
        }

        [Fact]
        public void PerformanceTest()
        {
            MyClass obj;

            var sw = Stopwatch.StartNew();
            for (var i = 0; i < 10000; ++i)
                obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON, new NetCoreDictionaryToArrayJsonConverter());
            sw.Stop();
            _testOutputHelper.WriteLine($"Elapsed: {sw.Elapsed.TotalMilliseconds}ms");
            Assert.True(sw.Elapsed.TotalMilliseconds < 200);

            sw.Restart();
            for (var i = 0; i < 10000; ++i)
                obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON_2);
            sw.Stop();
            _testOutputHelper.WriteLine($"Elapsed: {sw.Elapsed.TotalMilliseconds}ms");
            Assert.True(sw.Elapsed.TotalMilliseconds < 60);
        }

        [Fact]
        public void BasicTest()
        {
            MyClass obj;

            obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON, new NetCoreDictionaryToArrayJsonConverter());
            var output1 = JsonConvert.SerializeObject(obj);
            _testOutputHelper.WriteLine($"{output1}");

            obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON_2);
            var output2 = JsonConvert.SerializeObject(obj);
            _testOutputHelper.WriteLine($"{output2}");

            Assert.Equal(output1, output2);
        }

        [Fact]
        public void NullabilityTest()
        {
            MyClass obj;

            obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON_N, new NetCoreDictionaryToArrayJsonConverter());
            var output1 = JsonConvert.SerializeObject(obj);
            _testOutputHelper.WriteLine($"{output1}");

            obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON_N2);
            var output2 = JsonConvert.SerializeObject(obj);
            _testOutputHelper.WriteLine($"{output2}");

            Assert.Equal(output1, output2);
        }

        [Fact]
        public void ComplexObjectTest()
        {
            MyClass3 obj2 = JsonConvert.DeserializeObject<MyClass3>(_RAW_JSON_M, new NetCoreDictionaryToArrayJsonConverter());
            var output1 = JsonConvert.SerializeObject(obj2);
            _testOutputHelper.WriteLine(output1);

            obj2 = JsonConvert.DeserializeObject<MyClass3>(_RAW_JSON_M2);
            var output2 = JsonConvert.SerializeObject(obj2);
            _testOutputHelper.WriteLine(output2);

            Assert.Equal(output1, output2);
        }
    }
}
