using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AT.Common;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace AT.NUnitTests.NET5
{
    [TestFixture]
    public class AtClientTests
    {
        private IAtClient AtClient { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
            AtClient = new AtClient(new AtClientOptions
            {
                AtHost = "http://localhost:29300/inias/csc/autotests/auto-tests/service/auto-tests/execute/"
            }, new HttpClient());
        }

        [Test(Description = "Test test")]
        public void TestPass()
        {
            Assert.Pass();
        }

        [Test(Description = "Example test with 2 parameters.")]
        public async Task TwoParamsTest()
        {
            var testId = Guid.NewGuid();
            var publishers = new[] { "http", "kafka" };

            var response = await AtClient.RunTest(testId, "CorLibBundleAutoTestExample", publishers,
                new { Param1 = "test string" });
        
            Assert.NotNull(response);

            if (response is not null)
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
            else
            {
                Assert.Fail("Объект HttpResponse равен null.");
            }
        }

        [Test(Description = "Example test with random result.")]
        public async Task RandomTest()
        {
            var testId = Guid.NewGuid();
            var publishers = new[] { "http", "kafka" };

            var response = await AtClient.RunTest(testId, "CorLibBundleAutoTestRandomExample", publishers, new { });
            var expectedTestResult = "Success";

            Assert.NotNull(response);

            if (response is null)
            {
                return;
            }

            var testResult =
                ((JObject?)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()))?.Value<string>(
                    "TestResult");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(testResult, Is.EqualTo(expectedTestResult).IgnoreCase);
        }
    }
}