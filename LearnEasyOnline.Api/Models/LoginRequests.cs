using System.ComponentModel.DataAnnotations;

namespace LearnEasyOnline.Api.Models
{
    public class LoginRequests
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
