using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheck
{
    public class HealthChecks : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            bool isHealthy = false;
            string descriptionHealthy = "My API is healthy";
            string descriptionNotHealthy = "My API is not healthy";
            try
            {
                isHealthy = true;
                return HealthCheckResult.Healthy(descriptionHealthy);

            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(descriptionNotHealthy);
            }
        }
    }
}

