using KoerberExercise.Data.Models.Implementations;
using KoerberExercise.Data.Models.Interfaces;
using KoerberExercise.Logic.Validation;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace KoerberExercise.Logic.Models.Machine
{
    public class MachineCreateDto : IParent
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int? ParentId { get; set; }

        [ValidEnum]
        [JsonProperty(Required = Required.Always)]
        public MachineType Type { get; set; }

        public bool? Enabled { get; set; }
    }
}
