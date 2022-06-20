using KoerberExercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace KoerberExercise.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;
        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        /// <summary>
        ///     Get Health
        /// </summary>
        /// <remarks>Provides an indication about the health of the API</remarks>
        /// <response code="200">API is healthy</response>
        /// <response code="503">API is unhealthy or in degraded state</response>
        [HttpGet]
        public async Task<ActionResult<HealthReportDto>> Get()
        {
            var report = await _healthCheckService.CheckHealthAsync();

            var reportDto = new HealthReportDto
            {
                Status = report.Status,
                TotalDuration = report.TotalDuration,
                Entries = report.Entries.Select(e => new EntryModel { Key = e.Key, Description = e.Value.Description, Status = e.Value.Status })
            };

            return reportDto.Status == HealthStatus.Unhealthy ? StatusCode((int)HttpStatusCode.ServiceUnavailable, reportDto) : Ok(reportDto);
        }
    }
}
