using Entity.DTO.Operational;
using Microsoft.AspNetCore.Mvc;
using Service.Operational.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Controllers.Operational.Interface;

namespace Web.Controllers.Operational.Implements
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectorFarmController : ControllerBase, ICollectorFarmController
    {
        private readonly ICollectorFarmService _collectorFarmService;

        public CollectorFarmController(ICollectorFarmService collectorFarmService)
        {
            _collectorFarmService = collectorFarmService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollectorFarmDto>>> GetAll()
        {
            var collectorFarms = await _collectorFarmService.GetAll();
            return Ok(collectorFarms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CollectorFarmDto>> GetById(int id)
        {
            var collectorFarm = await _collectorFarmService.GetById(id);
            if (collectorFarm == null)
            {
                return NotFound();
            }
            return Ok(collectorFarm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CollectorFarmDto collectorFarmDto)
        {
            await _collectorFarmService.Add(collectorFarmDto);
            return CreatedAtAction(nameof(GetById), new { id = collectorFarmDto.Id }, collectorFarmDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(CollectorFarmDto collectorFarmDto)
        {
            await _collectorFarmService.Update(collectorFarmDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _collectorFarmService.Delete(id);
            return NoContent();
        }
    }
}
