using System;
using System.Collections.Generic;
using System.Globalization;
using XPike.Configuration.Memory;
using Xunit;

namespace XPike.Configuration.Tests
{
    public class ConfigurationServiceTests
    {
        private const string _EXPECTED_NAME = "Some Name Here";

        private readonly DateTime _testDateTime = new DateTime(2019, 10, 27, 20, 52, 33);
        private readonly TimeSpan _testTimeSpan = TimeSpan.FromSeconds(1337);
        private readonly int _testInt = 1337;
        private readonly decimal _testDecimal = (decimal) 170.1;

        private string GetJson() =>
            $"{{\"Name\":\"{_EXPECTED_NAME}\",\"Time\":\"{_testDateTime.ToString(CultureInfo.InvariantCulture)}\"}}";

        private IConfigurationService CreateService() =>
            new XPikeConfigBuilder()
                .AddProvider(new MemoryConfigurationProvider(
                    new Dictionary<string, string>
                    {
                        {"Some.Library.Config", $"{GetJson()}"},
                        {"Some.DateTime", _testDateTime.ToString(CultureInfo.InvariantCulture)},
                        {"Some.Bool", true.ToString()},
                        {"Some.Int", _testInt.ToString()},
                        {"Some.TimeSpan", _testTimeSpan.ToString()},
                        {"Some.Decimal", _testDecimal.ToString(CultureInfo.InvariantCulture)}
                    }))
                .Build();

        private class TestObject
        {
            public string Name { get; set; }
            public DateTime Time { get; set; }
        }

        [Fact]
        public void Test_GetObject()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var obj = service.GetValue<TestObject>("Some.Library.Config");

            Assert.NotNull(obj);
            Assert.Equal(obj.Name, _EXPECTED_NAME);
            Assert.Equal(obj.Time, _testDateTime);
        }

        [Fact]
        public void Test_GetObject_CaseSensitive()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var obj = service.GetValueOrDefault<TestObject>("some.library.config");

            Assert.Null(obj);
        }

        [Fact]
        public void Test_GetObjectJson()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var str = service.GetValue<string>("Some.Library.Config");

            Assert.NotNull(str);
            Assert.Equal(str, GetJson());
        }

        [Fact]
        public void Test_GetDateTime()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var dt = service.GetValue<DateTime>("Some.DateTime");

            Assert.Equal(dt, _testDateTime);
        }

        [Fact]
        public void Test_GetBool()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var b = service.GetValue<bool>("Some.Bool");

            Assert.True(b);
        }

        [Fact]
        public void Test_GetInt()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var i = service.GetValue<int>("Some.Int");

            Assert.Equal(i, _testInt);
        }

        [Fact]
        public void Test_GetDecimal()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var d = service.GetValue<decimal>("Some.Decimal");

            Assert.Equal(d, _testDecimal);
        }

        [Fact]
        public void Test_GetTimeSpan()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var ts = service.GetValue<TimeSpan>("Some.TimeSpan");

            Assert.Equal(ts, _testTimeSpan);
        }
    }
}
