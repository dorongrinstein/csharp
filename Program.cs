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

app.Urls.Clear();
app.Urls.Add("http://+:8080");

Console.WriteLine("Starting server on port 8080");
app.Run();

record SumRequest(int a, int b);

