using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace KoerberExercise.Models
{
    public class EntryModel
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public HealthStatus Status { get; set; }
    }
}
