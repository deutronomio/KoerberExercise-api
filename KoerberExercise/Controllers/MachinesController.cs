using KoerberExercise.Logic.Models.Machine;
using KoerberExercise.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoerberExercise.Controllers
{
    [ApiController]
    [Route("api/machines")]//"api/[controller]")]
    public class MachinesController : ControllerBase
    {

        private readonly IMachinesService _machinesService;

        public MachinesController(IMachinesService machinesService)
        {
            _machinesService = machinesService;
        }

        //Get api/machines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineReadDto>>> GetAll()
        {
            var machines = await _machinesService.GetAllAsync();
            return Ok(machines);
        }

        //Get api/machines/{id}
        [HttpGet("{id}.{format?}", Name = "GetById"), FormatFilter]
        public async Task<ActionResult<MachineReadDto>> GetById(int id)
        {
            var machine = await _machinesService.GetAsync(id);
            if (machine != null)
                return Ok(machine);

            return NotFound();
        }

        //Post api/machines
        [HttpPost]
        public async Task<ActionResult<MachineReadDto>> Create(MachineCreateDto machineCreate)
        {
            int? id = await _machinesService.AddAsync(machineCreate);
            if(id.HasValue == false)
                return BadRequest();

            var machine = await _machinesService.GetAsync(id.Value);
            return CreatedAtRoute(nameof(GetById), new { Id = machine.Id }, machine);
        }

        //Put api/machines/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, MachineUpdateDto machineUpdate)
        {
            if ((await _machinesService.ExistAsync(id)) == false)
                return NotFound();

            bool result = await _machinesService.UpdateAsync(id, machineUpdate);
            if (result == false)
                return BadRequest();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if ((await _machinesService.ExistAsync(id)) == false)
                return NotFound();
            
            if (await _machinesService.DeleteAsync(id) == false)
                return BadRequest();

            return NoContent();
        }
    }
}