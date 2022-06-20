using KoerberExercise.Data.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoerberExercise.Logic.Models.Machine
{
    public class MachineReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public MachineType Type { get; set; }

        public DateTime LastModified { get; set; }

        public bool Enabled { get; set; }
    }
}
