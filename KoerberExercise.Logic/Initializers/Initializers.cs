using KoerberExercise.Data.Contexts;
using Microsoft.AspNetCore.Builder;

namespace KoerberExercise.Logic.Initializers
{
    public static class Initializers
    {
        public static void Populate(IApplicationBuilder app)
        {
            PrepDB.PrepAndPopulate(app);
        }
    }
}
