using AutoMapper;
using KoerberExercise.Data.Models.Implementations;
using KoerberExercise.Logic.Models.Machine;

namespace KoerberExercise.Logic.Profiles
{
    public class MachinesProfile : Profile
    {
        public MachinesProfile()
        {
            CreateMap<MachineEntity, MachineReadDto>();
            CreateMap<MachineCreateDto, MachineEntity>().ForMember(o => o.Enabled, o => o.NullSubstitute(true));
            CreateMap<MachineUpdateDto, MachineEntity>().ForMember(o => o.Enabled, o => o.NullSubstitute(true));

        }
    }
}
