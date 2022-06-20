using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace KoerberExercise.Models
{
    public class HealthReportDto
    {
        public IEnumerable<EntryModel> Entries { get; set; }

        public HealthStatus Status { get; set; }

        public TimeSpan TotalDuration { get; set; }
    }
}
