using ExcellenceHubAPI.DTOs;
using ExcellenceHubAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExcellenceHubAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Authentication : Controller
    {
        private static List<User> users = new List<User>();
        private static int nextId = 1;

        private readonly PasswordHasher<User> _passwordHasher;

        public Authentication()
        {
            _passwordHasher = new PasswordHasher<User>();
        }

        // ================= REGISTER =================

        [HttpPost("register")]
        public IActionResult Register(RegisterDto request)
        {
            // Model validation (automatic because of [ApiController])
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if contact number already exists
            if (users.Any(u => u.ContactNumber == request.ContactNumber))
                return BadRequest("User with this contact number already exists.");

            var user = new User
            {
                Id = nextId++,
                FirstName = request.FirstName,
                LastName = request.LastName,
                ContactNumber = request.ContactNumber
            };

            // Hash password
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            users.Add(user);

            return Ok("User registered successfully.");
        }

        // ================= LOGIN =================

        [HttpPost("login")]
        public IActionResult Login(LoginDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized("Invalid contact number or password.");

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password
            );

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid contact number or password.");

            return Ok("Login successful.");
        }
    }
}
