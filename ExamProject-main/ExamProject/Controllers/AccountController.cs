using AutoMapper;
using ExamProject.Application.DTOs.AccountDTOs;
using ExamProject.Application.Interfaces.IUnitOfWorks;
using ExamProject.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace ExamProject.API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly IEmailSender emailSender;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AccountController(UserManager<ApplicationUser> _userManager
            , IConfiguration _config, IEmailSender _emailSender, IMapper _mapper, IUnitOfWork _unitOfWork) {
            userManager = _userManager;
            config = _config;
            emailSender = _emailSender;
            mapper = _mapper;
            unitOfWork = _unitOfWork;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = mapper.Map<ApplicationUser>(userDto);
            var result = await userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            await userManager.AddToRoleAsync(user, "student");
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            //var encodedToken = HttpUtility.UrlEncode(token);
            //var encodedToken =   Convert.ToBase64String(Encoding.UTF8.GetBytes(token))
            //    .Replace("+", "-")
            //     .Replace("/", "_")
            //      .Replace("=", "");
            var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            var confirmationLink = $"http://localhost:4200/Account/ConfirmEmail?userId={user.Id}&token={encodedToken}";
            await emailSender.SendEmailAsync(user.Email, "Confirm your Email", $"Please confirm your account by clicking this link: <a href=\"{confirmationLink}\">Confirm</a>");
            var examIds = unitOfWork.ExamRepo.GetIdsForAllExams();
            foreach (var examId in examIds) {
                var userExam = new UserExamEntity();
                userExam.Id = user.Id;
                userExam.ExamId = examId;
                await unitOfWork.UserExamRepo.AddAsync(userExam);
            }

            await unitOfWork.SaveChangesAsync();
            return Ok(new {messege  = "Registration successful! Please confirm your email before logging in." });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token) {
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
                return BadRequest("Invalid user");
            //string base64 = token.Replace("-", "+").Replace("_", "/");
            //switch (token.Length % 4)
            //{
            //    case 2: base64 += "=="; break;
            //    case 3: base64 += "="; break;
            //}
            string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var result = await userManager.ConfirmEmailAsync(user, decodedToken);
            if (result.Succeeded)
            {
                var signInToken = GenerateJwtToken(user);
                return Ok(new { signInToken });

            }
            return BadRequest("Email confirmation failed");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByNameAsync(loginDto.UserName);
            if (user is null)
                return Unauthorized(new {message = "Invalid User" });

            if (!await userManager.IsEmailConfirmedAsync(user))
                return Unauthorized(new {message = "Email is not confirmed" });

            if (await userManager.CheckPasswordAsync(user, loginDto.Password)) {
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            return Unauthorized(new {message = "Invalid Password" });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout() {
            return Ok("Logged out successfully.");
        }
        [Authorize] 
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changepPassDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue("id");
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return Unauthorized();

            var result = await userManager.ChangePasswordAsync(user, changepPassDto.CurrentPassword, changepPassDto.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    
                    ModelState.AddModelError("CurrentPassword", error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok(new {message = "Password changed successfully." });
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPassDto) {
            var user = await userManager.FindByEmailAsync(forgotPassDto.Email);
            if (user is null || !(await userManager.IsEmailConfirmedAsync(user)))
            {
                ModelState.AddModelError("Email", "Email Not Found");
                return BadRequest(ModelState);
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            var resetLink = $"http://localhost:4200/Account/ResetPassword?email={user.Email}&token={encodedToken}";
            await emailSender.SendEmailAsync(user.Email, "Reset your Password", $"Please reset your Password by clicking this link: <a href=\"{resetLink}\">Reset</a>");
            return Ok(new { message = "Password reset link has been sent to your email." });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassDto) {
            var user = await userManager.FindByEmailAsync(resetPassDto.Email);
            if (user is null)
            {
                ModelState.AddModelError("Email", "Email not found");
                return BadRequest(ModelState);
            }
            string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(resetPassDto.Token));

            var result = await userManager.ResetPasswordAsync(user, decodedToken, resetPassDto.NewPassword);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(new {message = "Password has been reset successfully" });
        }

        private string GenerateJwtToken(ApplicationUser user) {
            var claims = new List<Claim>()
            {
                new Claim("userName", user.UserName) ,
                new Claim("id",user.Id.ToString()) ,
               
            };
            var roles =  userManager.GetRolesAsync(user)?.Result;
            if (roles.Any())
            {
                claims.Add(new Claim("role", roles[0]));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(20),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}