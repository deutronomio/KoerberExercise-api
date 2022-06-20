using KoerberExercise.Logic.Services.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace KoerberExercise.HealthChecks
{
    public class LoadHealthCheck : IHealthCheck
    {
        private readonly IMachinesService _machinesService;

        public LoadHealthCheck(IMachinesService machinesService)
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
                var result = await _machinesService.GetFirstAsync();
                stopwatch.Stop();

                responseTimeInMS = stopwatch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(
                    description: $"Failed to load machine.",
                    exception: ex
                );
            }

            if (responseTimeInMS < 100)
                return HealthCheckResult.Healthy($"Loading machine is healthy (time: {responseTimeInMS} ms).");
            else if (responseTimeInMS < 200)
                return HealthCheckResult.Degraded($"Loading machine is bit slow (time: {responseTimeInMS} ms).");
            else
                return HealthCheckResult.Unhealthy($"Loading machine is too slow (time: {responseTimeInMS} ms).");
        }
    }
}
