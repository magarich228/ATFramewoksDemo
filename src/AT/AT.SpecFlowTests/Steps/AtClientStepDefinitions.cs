using System.Net;
using AT.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace AT.SpecFlowTests.Steps;

[Binding]
public sealed class AtClientStepDefinitions
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;
    private readonly IAtClient _atClient;
    private HttpResponseMessage? _response;
    
    public AtClientStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _atClient = new AtClient(new AtClientOptions
        {
            AtHost = "http://localhost:29300/inias/csc/autotests/auto-tests/service/auto-tests/execute/"
        }, new HttpClient());
    }

    [Given("Invoke random test")]
    [When("Invoke random test")]
    public async Task InvokeRandomTest()
    {
        var testId = Guid.NewGuid();
        var publishers = new[] { "http", "kafka" };

        var response = await _atClient.RunTest(testId, "CorLibBundleAutoTestRandomExample", publishers, new { }) ??
                       throw new Exception("A query to invoke a random test returned an null response.");

        _response = response;
    }

    [Then("Response status code is ([1-5][0-9]{2})")]
    public void ResponseStatusCodeIs(int statusCode)
    {
        Assert.IsNotNull(_response);
        
        if (_response is not null)
            Assert.That(_response?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Then("Response test result is (.*)")]
    public async Task ResponseTestResultIs(string expectedTestResult)
    {
        Assert.IsNotNull(_response);

        if (_response is not null)
        {
            var testResult =
                ((JObject?)JsonConvert.DeserializeObject(await _response!.Content.ReadAsStringAsync()))?.Value<string>(
                    "TestResult");

            Console.WriteLine(testResult);
            StringAssert.AreEqualIgnoringCase(expectedTestResult, testResult);
        }
    }
}