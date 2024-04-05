using System.Net;
using AT.BDTests.Features;
using BDTest.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AT.BDTests.Steps;

public class AtClientStepDefinitions
{
    private readonly AtClientContext _context;

    public AtClientStepDefinitions(AtClientContext context)
    {
        _context = context;
    }

    [StepText("Invoke random test")]
    public async Task InvokeRandomTest()
    {
        var testId = Guid.NewGuid();
        var publishers = new[] { "http", "npgsql", "kafka" };

        var response = await _context.AtClient.RunTest(testId, "CorLibBundleAutoTestRandomExample", publishers, new { }) ??
                       throw new Exception("A query to invoke a random test returned an null response.");

        _context.Response = response;
    }
    
    [StepText("Response status code is {0}")]
    public void ResponseStatusCodeIs(HttpStatusCode statusCode)
    {
        Assert.IsNotNull(_context.Response);
        
        if (_context.Response is not null)
            Assert.That(_context.Response?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [StepText("Response test result is {0}")]
    public async Task ResponseTestResultIs(string expectedTestResult)
    {
        Assert.IsNotNull(_context.Response);

        if (_context.Response is not null)
        {
            var testResult =
                ((JObject?)JsonConvert.DeserializeObject(await _context.Response!.Content.ReadAsStringAsync()))?.Value<string>(
                    "TestResult");

            Console.WriteLine(testResult);
            StringAssert.AreEqualIgnoringCase(expectedTestResult, testResult);
        }
    }
}
