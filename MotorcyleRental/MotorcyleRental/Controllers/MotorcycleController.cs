using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcyleRental.Models;
using MotorcyleRental.Services.Interfaces;

namespace MotorcyleRental.Controllers
{
  
    [ApiController]
    [Route("[controller]")]
    public class Motorcyclecontroller : ControllerBase
    {
      

        private readonly IMotorcycleService _service;

        public Motorcyclecontroller(IMotorcycleService service)
        {
            _service = service;
        }

        [HttpGet("GetMotorcycles")]
        public async Task<IEnumerable<MotorCycleViewModel>> Get(string filter = "")
        {
            var motorcycle = await _service.GetAsync(filter);

            return motorcycle;
           
        }
        [HttpPost("AddMotorcycles")]
        public async Task<IActionResult> Add([FromBody] MotorCycleViewModel motorcycle)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Madotory fields: Id, Plate, Factoring Year, Model");
                }
                await _service.AddAsync(motorcycle);
                return Ok("Sucessfull Registration ");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpPut("EditMotorcycles")]
        public async Task<IActionResult> Edit([FromBody] MotorCycleViewModel motorcycle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Madotory fields: Id, Plate, Factoring Year, Model");
            }
            return Ok(await _service.EditAsync(motorcycle));
        }
        [HttpDelete("DeleteMotorcycles")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

    }
}
