using KoerberExercise.Logic.Services.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace KoerberExercise.HealthChecks
{
    public class DbAvailableHealthCheck : IHealthCheck
    {
        private readonly IMachinesService _machinesService;

        public DbAvailableHealthCheck(IMachinesService machinesService)
        {
            _machinesService = machinesService;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            long responseTimeInMS = 0;

            try
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();
                bool connected = await _machinesService.ExistAsync(3);
                stopwatch.Stop();

                responseTimeInMS = stopwatch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(
                    description: $"Failed to connect to DB.",
                    exception: ex
                );
            }

            if (responseTimeInMS < 100)
                return HealthCheckResult.Healthy($"Connection to DB is healthy (time: {responseTimeInMS} ms).");
            else if (responseTimeInMS < 200)
                return HealthCheckResult.Degraded($"Connection to DB is bit slow (time: {responseTimeInMS} ms).");
            else
                return HealthCheckResult.Unhealthy($"Connection to DB is too slow (time: {responseTimeInMS} ms).");
        }
    }
}
