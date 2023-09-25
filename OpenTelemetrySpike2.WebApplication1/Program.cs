var builder = WebApplication.CreateBuilder(args);
builder.Services
	.AddHostedService<OpenTelemetrySpike2.WebApplication1.Worker>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
