using System.Net;
using AT.BDTests.Steps;
using AT.Common;
using BDTest.Attributes;
using BDTest.NUnit;

namespace AT.BDTests.Features;

public class AtClientContext
{
    public readonly IAtClient AtClient = new AtClient(new AtClientOptions
    {
        AtHost = "http://localhost:29300/inias/csc/autotests/auto-tests/service/auto-tests/execute/"
    }, new HttpClient());
    
    public HttpResponseMessage? Response;
}

[Story(AsA = "Kirill Groshev",
    IWant = "invoke random test",
    SoThat = "Demonstrate BDTest")]
public class AtClientTests : NUnitBDTestBase<AtClientContext>
{
    private AtClientStepDefinitions AtClientSteps => new(Context);
    
    [Test]
    [ScenarioText("Invoke random test.")]
    public async Task InvokeRandomTest()
    {
        var steps = AtClientSteps;
        
        await When(() => steps.InvokeRandomTest())
            .Then(() => steps.ResponseStatusCodeIs(HttpStatusCode.OK))
            .And(() => steps.ResponseTestResultIs("Success"))
            .BDTestAsync();
    }
}