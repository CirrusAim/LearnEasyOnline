using Microsoft.AspNetCore.Mvc;

namespace LearnEasyOnline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        //  private readonly IPaymentService _paymentService; // Use this later

        //  public PaymentsController(IPaymentService paymentService)
        //  {
        //      _paymentService = paymentService;
        //  }

        [HttpPost("purchase")]
        public IActionResult Purchase([FromBody] PurchaseRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //  var result = _paymentService.Pay(request.Amount); // Use this later
                //  if (result.Success)
                //  {
                //      return Ok(new { Confirmation = result.ConfirmationNumber });
                //  }
                //  else
                //  {
                //      return BadRequest(new { Message = result.ErrorMessage });
                //  }

                // Simulate success for now
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
        // public decimal Amount { get; set; } // Remove Amount.  Get from CourseId
    }

    //  public interface IPaymentService  //use this later
    //  {
    //      PaymentResult Pay(decimal amount);
    //  }

    //  public class PaymentResult  //use this later
    //  {
    //      public bool Success { get; set; }
    //      public string? ConfirmationNumber { get; set; }
    //      public string? ErrorMessage { get; set; }
    //  }

    //  public class SomeFakePaymentAPI : IPaymentService //use this later
    //  {
    //      public PaymentResult Pay(decimal amount)
    //      {
    //          // Simulate a successful payment
    //          return new PaymentResult
    //          {
    //              Success = true,
    //              ConfirmationNumber = Guid.NewGuid().ToString()
    //          };
    //      }
    //  }

}
