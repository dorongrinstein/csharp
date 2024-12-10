var builder = WebApplication.CreateBuilder(args);

// Add this to suppress info messages
builder.Logging.SetMinimumLevel(LogLevel.Warning);

var app = builder.Build();

static object processSum(SumRequest req)
{
    Console.WriteLine("Processing sum of " + req.a + " and " + req.b);
    return new { sum = req.a + req.b };
}

app.MapPost("/sum", (SumRequest req) => processSum(req));

app.MapGet("/", () =>
{
    string host = Environment.GetEnvironmentVariable("CPLN_GLOBAL_ENDPOINT") ?? "localhost:8080";
    return $"curl --location 'https://{host}/sum' \\\n--header 'Content-Type: application/json' \\\n--data '{{\\"a\\":2, \\"b\\":4}}'";
});

app.Urls.Clear();
app.Urls.Add("http://+:8080");

Console.WriteLine("Starting server on port 8080");
app.Run();

record SumRequest(int a, int b);

