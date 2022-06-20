using KoerberExercise.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoerberExercise.Data.Models.Implementations
{
    public class MachineEntity: IParent, IUpdateInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)] 
        public string Name { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        public int? ParentId { get; set; }
        public MachineEntity Parent { get; set; }

        public MachineType Type { get; set; }

        public DateTime LastModified { get; set; }

        public bool Enabled { get; set; }
    }
}
