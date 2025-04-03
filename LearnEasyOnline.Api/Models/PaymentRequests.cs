using System.ComponentModel.DataAnnotations;

namespace LearnEasyOnline.Api.Models
{
    public class PaymentRequests
    {
        [Required(ErrorMessage = "CourseId is required")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "PaymentMethod is required")]
        public string PaymentMethod { get; set; }
    }
}
