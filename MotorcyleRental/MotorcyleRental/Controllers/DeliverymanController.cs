using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcyleRental.Models;
using MotorcyleRental.Services.Interfaces;

namespace MotorcyleRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliverymanController : ControllerBase
    {

        private readonly IDeliverymanService _deliverymanService;
        private readonly string _storagePath;

        public DeliverymanController(IDeliverymanService deliverymanService)
        {
            _deliverymanService = deliverymanService;
            _storagePath =  "cnh_uploads";
            Directory.CreateDirectory(_storagePath);
        }

        [HttpPost("CreateDeliveryman")]
        public async Task<IActionResult> CreateDeliveryman([FromBody] DeliverymanViewModel deliveryman)
        {
            try
            {
                await _deliverymanService.CreateDeliveryman(deliveryman);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("upload-cnh")]
        public async Task<IActionResult> UploadCNH(int id, IFormFile file)
        {
            if (file == null || (file.ContentType != "image/png" && file.ContentType != "image/bmp"))
            {
                return BadRequest("Invalid file format. Only PNG and BMP are accepted.");
            }

            var deliveryman = await _deliverymanService.GetDeliverymanById(id);
            if (deliveryman == null)
            {
                return NotFound("Delivery person not found.");
            }

            var filePath = Path.Combine(_storagePath, $"{id}_{file.FileName}");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await _deliverymanService.UpdateCNHImage(id, filePath);

            return Ok(new { FilePath = filePath });
        }
    }
}
