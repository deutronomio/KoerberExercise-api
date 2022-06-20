using AutoMapper;
using KoerberExercise.Data.Models.Implementations;
using KoerberExercise.Data.Models.Interfaces;
using KoerberExercise.Data.Repositories.Interfaces;
using KoerberExercise.Data.UnitOfWork.Interfaces;
using KoerberExercise.Logic.Models.Machine;
using KoerberExercise.Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoerberExercise.Logic.Services.Implementations
{
    public class MachinesService : IMachinesService
    {
        private readonly IMachinesRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MachinesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = unitOfWork.MachinesRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MachineReadDto>> GetAllAsync()
        {
            var machines = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MachineReadDto>>(machines);
        }

        public async Task<bool> ExistAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException($"{nameof(id)} is not valid.");

            return await _repository.ExistAsync(id);
        }

        public async Task<MachineReadDto> GetAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException($"{nameof(id)} is not valid.");

            MachineEntity machine = await _repository.GetAsync(id);
            if (machine != null)
                return _mapper.Map<MachineReadDto>(machine);

            return null;
        }

        public async Task<int?> AddAsync(MachineCreateDto machineCreate)
        {
            if (machineCreate == null)
                throw new ArgumentNullException(nameof(machineCreate));

            MachineEntity machineModel = _mapper.Map<MachineEntity>(machineCreate);

            await ValidateParent(machineModel.ParentId);

            UpdateDate(machineModel);

            _repository.Add(machineModel);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? machineModel.Id : null;
        }

        public async Task<bool> UpdateAsync(int id, MachineUpdateDto commandUpdate)
        {
            if (id < 1)
                throw new ArgumentException($"{nameof(id)} is not valid.");

            if (commandUpdate == null)
                throw new ArgumentNullException(nameof(commandUpdate));
            
            if(IsParentSameAsMachine(id, commandUpdate.ParentId))
                throw new ValidationException(
                    new ValidationResult($"{nameof(commandUpdate.ParentId)} cannot be the same as {nameof(id)}.", new List<string>() { nameof(id), nameof(commandUpdate.ParentId) }), null, null
                );

            await ValidateParent(commandUpdate.ParentId);

            var commandFromRpo = await _repository.GetAsync(id);
            if (commandFromRpo == null)
                return false;

            _mapper.Map(commandUpdate, commandFromRpo);

            UpdateDate(commandFromRpo);

            _repository.Update(commandFromRpo);

            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 1)
                throw new ArgumentException($"{nameof(id)} is not valid.");

            var commandFromRpo = await _repository.GetAsync(id);
            if (commandFromRpo == null)
                return false;

            if(await IsMachineAParent(id))
                throw new ValidationException(new ValidationResult($"{nameof(id)} {id} cannot be deleted because is a parent to another machine.", new List<string>() { nameof(id) }), null, null);

            _repository.Remove(commandFromRpo);

            return await _unitOfWork.SaveChangesAsync();
        }

        protected static bool IsParentSameAsMachine(int id, int? parentId)
        {
            if (id < 1)
                throw new ArgumentException($"{nameof(id)} is not valid.");

            return parentId.HasValue && id == parentId;
        }

        protected async Task ValidateParent(int? parentId)
        {
            if (parentId.HasValue && await _repository.ExistAsync(parentId.Value) == false)
                throw new ValidationException(new ValidationResult($"{nameof(parentId)} was not found.", new List<string>() { nameof(parentId) }), null, null);
        }

        public async Task<bool> IsMachineAParent(int id)
        {
            if (id < 1)
                throw new ArgumentException($"{nameof(id)} is not valid.");

            return await _repository.AnyAsync(x => x.ParentId != null && x.ParentId == id);
        }

        protected static void UpdateDate(IUpdateInfo model)
        {
            model.LastModified = DateTime.Now;
        }

        public async Task<MachineReadDto> GetFirstAsync()
        {
            MachineEntity machine = await _repository.FirstOrDefaultAsync();
            if (machine != null)
                return _mapper.Map<MachineReadDto>(machine);

            return null;
        }
    }
}