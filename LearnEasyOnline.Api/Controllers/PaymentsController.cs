using Microsoft.AspNetCore.Mvc;

namespace LearnEasyOnline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {

        [HttpPost("purchase")]
        public IActionResult Purchase([FromBody] PurchaseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                // Simulating success for now
                var confirmationNumber = Guid.NewGuid().ToString();
                return Ok(confirmationNumber);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class PurchaseRequest
    {
        public int CourseId { get; set; }
    }

}
