using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenTelemetry()
	.ConfigureResource(resource =>
	{
		resource.AddService(serviceName: builder.Environment.ApplicationName);
	})
	.WithMetrics(metrics =>
	{
		metrics
			.AddHttpClientInstrumentation()
			.AddPrometheusExporter();
	});
builder.Services
	.AddHttpClient<OpenTelemetrySpike2.WebApplication1.Services.IHttpBinClient, OpenTelemetrySpike2.WebApplication1.Services.Concrete.HttpBinClient>(client =>
	{
		client.BaseAddress = new Uri("https://httpbingo.org/");
	});
builder.Services
	.AddHostedService<OpenTelemetrySpike2.WebApplication1.Worker>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPrometheusScrapingEndpoint();

app.Run();
