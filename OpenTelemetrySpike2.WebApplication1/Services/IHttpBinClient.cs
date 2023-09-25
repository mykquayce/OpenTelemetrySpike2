namespace OpenTelemetrySpike2.WebApplication1.Services;

public interface IHttpBinClient
{
	Task<bool> UnstableAsync(double failureRate = 1, CancellationToken cancellationToken = default);
}
