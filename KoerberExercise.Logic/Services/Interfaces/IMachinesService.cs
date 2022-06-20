using KoerberExercise.Logic.Models.Machine;

namespace KoerberExercise.Logic.Services.Interfaces
{
    public interface IMachinesService
    {
        Task<IEnumerable<MachineReadDto>> GetAllAsync(); 
        Task<bool> ExistAsync(int id); 
        Task<MachineReadDto> GetAsync(int id); 
        Task<int?> AddAsync(MachineCreateDto machineCreate);
        Task<bool> UpdateAsync(int id, MachineUpdateDto commandUpdate); 
        Task<bool> DeleteAsync(int id);

        Task<bool> IsMachineAParent(int id);

        Task<MachineReadDto> GetFirstAsync();
    }
}