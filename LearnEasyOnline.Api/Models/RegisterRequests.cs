using System.ComponentModel.DataAnnotations;


namespace LearnEasyOnline.Api.Models
{
    public class RegisterRequests
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? PhoneNumber { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }
    }
}
