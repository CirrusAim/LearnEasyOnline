using Microsoft.AspNetCore.Identity;

namespace LearnEasyOnline.Api.Data
{
    public class Student : IdentityUser
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public int Age { get; set; }
    }
}
