using System.Text.Json;
using Microsoft.Playwright;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AT.PlaywrightTests;

[TestFixture]
public class AtClientTests : PlaywrightTest
{
    private IAPIRequestContext _apiRequestContext = null!;

    [Test]
    public async Task InvokeRandomTest()
    {
        var testId = Guid.NewGuid();
        var publishers = new[] { "http", "npgsql", "kafka" };
        var expectedTestResult = "Success";
        
        var response = await _apiRequestContext.PostAsync($"{testId}/CorLibBundleAutoTestRandomExample/{string.Join(',', publishers)}");
        var jsonBody = await response.TextAsync();
        var actualTestResult = ((JObject?)JsonConvert.DeserializeObject(jsonBody))?.Value<string>("TestResult");
        
        Assert.True(response.Ok);
        Assert.That(actualTestResult, Is.EqualTo(expectedTestResult));
    }
    
    [SetUp]
    public async Task SetUpAtContext()
    {
        _apiRequestContext = await Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = "http://localhost:29300/inias/csc/autotests/auto-tests/service/auto-tests/execute/"
        });
    }

    [TearDown]
    public async Task TearDownAtContext()
    {
        await _apiRequestContext.DisposeAsync();
    }
}