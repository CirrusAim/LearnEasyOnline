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
        private readonly ApplicationDbContext _context; // Add this
        private readonly ILogger<AuthController> _logger; // Add this




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
                _logger.LogError(ex, "Error during registration"); // Log the exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequests model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value?.Errors)
                           .Where(y => y != null)
                           .SelectMany(y => y!)
                           .Select(z => z.ErrorMessage)
                           .ToList();
                return BadRequest(new { Message = "Login failed", Errors = errors });
            }

            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null) //check if user is null
                    {
                        _logger.LogWarning("User not found for email: {Email}", model.Email);
                        return Unauthorized(new { Message = "User not found." });
                    }
                    var token = GenerateJwtToken(user);
                    return Ok(new { Message = "Login successful!", Token = token });
                }
                else
                {
                    _logger.LogInformation("Login failed for user: {Email}.  Succeeded: {Succeeded}, IsLockedOut: {IsLockedOut},  IsNotAllowed: {IsNotAllowed}", model.Email, result.Succeeded, result.IsLockedOut, result.IsNotAllowed);
                    return Unauthorized(new { Message = "Invalid login attempt." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string GenerateJwtToken(Student user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];

            if (string.IsNullOrEmpty(secretKey))  // Check if it's null or empty
            {
                _logger.LogError("JwtSettings:SecretKey is not configured.");
                throw new InvalidOperationException("JwtSettings:SecretKey is not configured.");
            }

            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.Email!), // Use '!'
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString()),
                 new Claim(ClaimTypes.Name, user.UserName!), // Use '!'
                 new Claim(ClaimTypes.Email, user.Email!),  // Use '!'
                 // Add other claims as needed (e.g., roles)
             };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!)); // Use '!'
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Adjust as needed
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
