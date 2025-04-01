using LearnEasyOnline.Api.Data;
using LearnEasyOnline.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace LearnEasyOnline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Student> _userManager;
        private readonly SignInManager<Student> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthController> _logger;




        public AuthController(UserManager<Student> userManager, SignInManager<Student> signInManager, IConfiguration configuration, ApplicationDbContext context, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequests model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value?.Errors)
                                           .Where(y => y != null)
                                           .SelectMany(y => y!)
                                           .Select(z => z.ErrorMessage)
                                           .ToList();
                return BadRequest(new { Message = "Registration failed", Errors = errors });
            }

            var user = new Student { UserName = model.Email, Email = model.Email, FullName = model.FullName, PhoneNumber = model.PhoneNumber, Age = model.Age };

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Registration successful!" });
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return BadRequest(new { Message = "Registration failed", Errors = errors });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
