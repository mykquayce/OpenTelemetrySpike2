namespace OpenTelemetrySpike2.WebApplication1;

public class Worker : BackgroundService
{
	private readonly ILogger<Worker> _logger;
	private readonly Services.IHttpBinClient _httpBinClient;

	public Worker(ILogger<Worker> logger, Services.IHttpBinClient httpBinClient)
	{
		_logger = logger;
		_httpBinClient = httpBinClient;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			var success = await _httpBinClient.UnstableAsync(failureRate: .5, stoppingToken);
			_logger.LogInformation("Worker running at: {time}: {success}", DateTimeOffset.Now, success);
			await Task.Delay(10_000, stoppingToken);
		}
	}
}
