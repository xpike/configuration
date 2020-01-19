using System;
using System.Collections.Generic;
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

        public class MyClassL
        {
            public List<int> MyArray { get; set; }
        }

        public class MyClassL3
        {
            public List<MyClass2> MyArray { get; set; }
        }

        public class MyClassE
        {
            public IEnumerable<int> MyArray { get; set; }
        }

        public class MyClassE3
        {
            public IEnumerable<MyClass2> MyArray { get; set; }
        }

        public class MyClassI
        {
            public IList<int> MyArray { get; set; }
        }

        public class MyClassI3
        {
            public IList<MyClass2> MyArray { get; set; }
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
                obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON, new AppSettingsArrayJsonConverter());
            sw.Stop();
            _testOutputHelper.WriteLine($"MS Array Elapsed: {sw.Elapsed.TotalMilliseconds}ms");
            Assert.True(sw.Elapsed.TotalMilliseconds < 200);

            sw.Restart();
            for (var i = 0; i < 10000; ++i)
                obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON_2);
            sw.Stop();
            _testOutputHelper.WriteLine($"Raw Array Elapsed: {sw.Elapsed.TotalMilliseconds}ms");
            Assert.True(sw.Elapsed.TotalMilliseconds < 60);

            sw.Restart();
            MyClassI iobj;
            for (var i = 0; i < 10000; ++i)
                iobj = JsonConvert.DeserializeObject<MyClassI>(_RAW_JSON, new AppSettingsArrayJsonConverter());
            sw.Stop();
            _testOutputHelper.WriteLine($"MS IList Elapsed: {sw.Elapsed.TotalMilliseconds}ms");

            sw.Restart();
            for (var i = 0; i < 10000; ++i)
                iobj = JsonConvert.DeserializeObject<MyClassI>(_RAW_JSON_2);
            sw.Stop();
            _testOutputHelper.WriteLine($"Raw IList Elapsed: {sw.Elapsed.TotalMilliseconds}ms");

            sw.Restart();
            MyClassL lobj;
            for (var i = 0; i < 10000; ++i)
                lobj = JsonConvert.DeserializeObject<MyClassL>(_RAW_JSON, new AppSettingsArrayJsonConverter());
            sw.Stop();
            _testOutputHelper.WriteLine($"MS List Elapsed: {sw.Elapsed.TotalMilliseconds}ms");

            sw.Restart();
            for (var i = 0; i < 10000; ++i)
                lobj = JsonConvert.DeserializeObject<MyClassL>(_RAW_JSON_2);
            sw.Stop();
            _testOutputHelper.WriteLine($"Raw List Elapsed: {sw.Elapsed.TotalMilliseconds}ms");

            sw.Restart();
            MyClassE eobj;
            for (var i = 0; i < 10000; ++i)
                eobj = JsonConvert.DeserializeObject<MyClassE>(_RAW_JSON, new AppSettingsArrayJsonConverter());
            sw.Stop();
            _testOutputHelper.WriteLine($"MS Enumerable Elapsed: {sw.Elapsed.TotalMilliseconds}ms");

            sw.Restart();
            for (var i = 0; i < 10000; ++i)
                eobj = JsonConvert.DeserializeObject<MyClassE>(_RAW_JSON_2);
            sw.Stop();
            _testOutputHelper.WriteLine($"Raw Enumerable Elapsed: {sw.Elapsed.TotalMilliseconds}ms");
        }

        [Fact]
        public void BasicTest()
        {
            var obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON, new AppSettingsArrayJsonConverter());
            var output1 = JsonConvert.SerializeObject(obj);
            _testOutputHelper.WriteLine($"{output1}");

            obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON_2);
            var output2 = JsonConvert.SerializeObject(obj);
            _testOutputHelper.WriteLine($"{output2}");

            Assert.Equal(output1, output2);

            var eobj = JsonConvert.DeserializeObject<MyClassE>(_RAW_JSON, new AppSettingsArrayJsonConverter());
            output1 = JsonConvert.SerializeObject(eobj);
            _testOutputHelper.WriteLine($"{output1}");

            eobj = JsonConvert.DeserializeObject<MyClassE>(_RAW_JSON_2);
            output2 = JsonConvert.SerializeObject(eobj);
            _testOutputHelper.WriteLine($"{output2}");

            Assert.Equal(output1, output2);

            var iobj = JsonConvert.DeserializeObject<MyClassI>(_RAW_JSON, new AppSettingsArrayJsonConverter());
            output1 = JsonConvert.SerializeObject(iobj);
            _testOutputHelper.WriteLine($"{output1}");

            iobj = JsonConvert.DeserializeObject<MyClassI>(_RAW_JSON_2);
            output2 = JsonConvert.SerializeObject(iobj);
            _testOutputHelper.WriteLine($"{output2}");

            Assert.Equal(output1, output2);

            var lobj = JsonConvert.DeserializeObject<MyClassL>(_RAW_JSON, new AppSettingsArrayJsonConverter());
            output1 = JsonConvert.SerializeObject(lobj);
            _testOutputHelper.WriteLine($"{output1}");

            lobj = JsonConvert.DeserializeObject<MyClassL>(_RAW_JSON_2);
            output2 = JsonConvert.SerializeObject(lobj);
            _testOutputHelper.WriteLine($"{output2}");

            Assert.Equal(output1, output2);
        }

        [Fact]
        public void NullabilityTest()
        {
            var obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON_N, new AppSettingsArrayJsonConverter());
            var output1 = JsonConvert.SerializeObject(obj);
            _testOutputHelper.WriteLine($"{output1}");

            obj = JsonConvert.DeserializeObject<MyClass>(_RAW_JSON_N2);
            var output2 = JsonConvert.SerializeObject(obj);
            _testOutputHelper.WriteLine($"{output2}");

            Assert.Equal(output1, output2);

            var eobj = JsonConvert.DeserializeObject<MyClassE>(_RAW_JSON_N, new AppSettingsArrayJsonConverter());
            output1 = JsonConvert.SerializeObject(eobj);
            _testOutputHelper.WriteLine($"{output1}");

            eobj = JsonConvert.DeserializeObject<MyClassE>(_RAW_JSON_N2);
            output2 = JsonConvert.SerializeObject(eobj);
            _testOutputHelper.WriteLine($"{output2}");

            Assert.Equal(output1, output2);

            var iobj = JsonConvert.DeserializeObject<MyClassI>(_RAW_JSON_N, new AppSettingsArrayJsonConverter());
            output1 = JsonConvert.SerializeObject(iobj);
            _testOutputHelper.WriteLine($"{output1}");

            iobj = JsonConvert.DeserializeObject<MyClassI>(_RAW_JSON_N2);
            output2 = JsonConvert.SerializeObject(iobj);
            _testOutputHelper.WriteLine($"{output2}");

            Assert.Equal(output1, output2);

            var lobj = JsonConvert.DeserializeObject<MyClassL>(_RAW_JSON_N, new AppSettingsArrayJsonConverter());
            output1 = JsonConvert.SerializeObject(lobj);
            _testOutputHelper.WriteLine($"{output1}");

            lobj = JsonConvert.DeserializeObject<MyClassL>(_RAW_JSON_N2);
            output2 = JsonConvert.SerializeObject(lobj);
            _testOutputHelper.WriteLine($"{output2}");

            Assert.Equal(output1, output2);
        }

        [Fact]
        public void ComplexObjectTest()
        {
            var obj2 = JsonConvert.DeserializeObject<MyClass3>(_RAW_JSON_M, new AppSettingsArrayJsonConverter());
            var output1 = JsonConvert.SerializeObject(obj2);
            _testOutputHelper.WriteLine(output1);

            obj2 = JsonConvert.DeserializeObject<MyClass3>(_RAW_JSON_M2);
            var output2 = JsonConvert.SerializeObject(obj2);
            _testOutputHelper.WriteLine(output2);

            Assert.Equal(output1, output2);

            var eobj2 = JsonConvert.DeserializeObject<MyClassE3>(_RAW_JSON_M, new AppSettingsArrayJsonConverter());
            output1 = JsonConvert.SerializeObject(eobj2);
            _testOutputHelper.WriteLine(output1);

            eobj2 = JsonConvert.DeserializeObject<MyClassE3>(_RAW_JSON_M2);
            output2 = JsonConvert.SerializeObject(eobj2);
            _testOutputHelper.WriteLine(output2);

            Assert.Equal(output1, output2);

            var iobj2 = JsonConvert.DeserializeObject<MyClassI3>(_RAW_JSON_M, new AppSettingsArrayJsonConverter());
            output1 = JsonConvert.SerializeObject(iobj2);
            _testOutputHelper.WriteLine(output1);

            iobj2 = JsonConvert.DeserializeObject<MyClassI3>(_RAW_JSON_M2);
            output2 = JsonConvert.SerializeObject(iobj2);
            _testOutputHelper.WriteLine(output2);

            Assert.Equal(output1, output2);

            var lobj2 = JsonConvert.DeserializeObject<MyClassL3>(_RAW_JSON_M, new AppSettingsArrayJsonConverter());
            output1 = JsonConvert.SerializeObject(lobj2);
            _testOutputHelper.WriteLine(output1);

            lobj2 = JsonConvert.DeserializeObject<MyClassL3>(_RAW_JSON_M2);
            output2 = JsonConvert.SerializeObject(lobj2);
            _testOutputHelper.WriteLine(output2);

            Assert.Equal(output1, output2);
        }
    }
}
