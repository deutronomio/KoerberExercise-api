using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoerberExercise.Data.Models.Interfaces
{
    public interface IUpdateInfo
    {
        DateTime LastModified { get; set; }
    }
}
