using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
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
        public async Task Test_GetObjectAsync()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var obj = await service.GetValueAsync<TestObject>("Some.Library.Config");

            Assert.NotNull(obj);
            Assert.Equal(obj.Name, _EXPECTED_NAME);
            Assert.Equal(obj.Time, _testDateTime);
        }

        [Fact]
        public void Test_GetObject_CaseSensitive()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var obj = service.GetValueOrDefaultAsync<TestObject>("some.library.config");

            Assert.Null(obj);
        }

        [Fact]
        public async Task Test_GetObjectJsonAsync()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var str = await service.GetValueAsync<string>("Some.Library.Config");

            Assert.NotNull(str);
            Assert.Equal(str, GetJson());
        }

        [Fact]
        public async Task Test_GetDateTimeAsync()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var dt = await service.GetValueAsync<DateTime>("Some.DateTime");

            Assert.Equal(dt, _testDateTime);
        }

        [Fact]
        public async Task Test_GetBoolAsync()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var b = await service.GetValueAsync<bool>("Some.Bool");

            Assert.True(b);
        }

        [Fact]
        public async Task Test_GetIntAsync()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var i = await service.GetValueAsync<int>("Some.Int");

            Assert.Equal(i, _testInt);
        }

        [Fact]
        public async Task Test_GetDecimalAsync()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var d = await service.GetValueAsync<decimal>("Some.Decimal");

            Assert.Equal(d, _testDecimal);
        }

        [Fact]
        public async Task Test_GetTimeSpanAsync()
        {
            var service = CreateService();
            Assert.NotNull(service);

            var ts = await service.GetValueAsync<TimeSpan>("Some.TimeSpan");

            Assert.Equal(ts, _testTimeSpan);
        }
    }
}
