using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcyleRental.Services;
using MotorcyleRental.Services.Interfaces;

namespace MotorcyleRental.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalsService _rentalService;

        public RentalsController(IRentalsService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost("CreateRental")]
        public async Task<IActionResult> CreateRental(int deliverymanId, int motorcycleId, int rentalPlanId)
        {
            try
            {
                await _rentalService.CreateRental(deliverymanId, motorcycleId, rentalPlanId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnRental(int rentalId, DateTime actualEndDate)
        {
            try
            {
                var totalCost = await _rentalService.CalculateTotalCost(rentalId, actualEndDate);
                return Ok(new { TotalCost = totalCost });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
