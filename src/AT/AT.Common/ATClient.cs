using System.Text;
using Newtonsoft.Json;

namespace AT.Common;

public class AtClientOptions
{
    public string AtHost { get; init; } = null!;
}

public class AtClient : IAtClient
{
    private readonly HttpClient _client;

    public AtClient(
        AtClientOptions options,
        HttpClient client)
    {
        var atHost = options.AtHost;
        _client = client;

        _client.BaseAddress = new Uri(atHost);
    }


    public async Task<HttpResponseMessage?> RunTest(Guid testId, string testType, IEnumerable<string> publishers, dynamic body) =>
        await _client.PostAsync(
            $"{testId}/{testType}/{string.Join(',', publishers)}", 
            new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8));
}

public interface IAtClient
{
    Task<HttpResponseMessage?> RunTest(Guid testId, string testType, IEnumerable<string> publishers, dynamic body);
}