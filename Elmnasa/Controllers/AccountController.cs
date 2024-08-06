using ElmnasaApp.Email.Intrefaces;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.JWTToken.Intrefaces;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.StudentDTOs;
using ElmnasaDomain.Entites.identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Elmnasa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSettings _emailSettings;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Student> _studentManager;
        private readonly SignInManager<Student> _signInManagerStudent;
        private readonly UserManager<Teacher> _TeacherManager;
        private readonly SignInManager<Teacher> _signInManagerTeacher;
        private readonly UserManager<Admin> _AdminManager;
        private readonly SignInManager<Admin> _signInManagerAdmin;
        private readonly UserManager<Account> _accountManager;
        private readonly SignInManager<Account> _signInManagerAccount;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<Student> studentManager,
        SignInManager<Student> signInManagerStudent,
        UserManager<Teacher> TeacherManager,
        SignInManager<Teacher> signInManagerTeacher,
         UserManager<Admin> AdminManager,
        SignInManager<Admin> signInManagerAdmin,
        UserManager<Account> accountManager,
        SignInManager<Account> signInManagerAccount,
        ITokenService tokenService,
            ILogger<AccountController> logger,
            IEmailSettings emailSettings,
            RoleManager<IdentityRole> roleManager
            )
        {
            _studentManager = studentManager;
            _signInManagerStudent = signInManagerStudent;
            _TeacherManager = TeacherManager;
            _signInManagerTeacher = signInManagerTeacher;
            _AdminManager = AdminManager;
            _signInManagerAdmin = signInManagerAdmin;
            _accountManager = accountManager;
            _signInManagerAccount = signInManagerAccount;
            _tokenService = tokenService;
            _logger = logger;
            _emailSettings = emailSettings;
            _roleManager = roleManager;
        }

        [HttpPost("StudentRegister")]
        public async Task<ActionResult<StudentDto>> Register(StudentReigsterDTO registerDto)
        {
            try
            {
                // Check if the Student already exists
                var studentExists = await CheckIfStudentExist(registerDto.Email);
                if (studentExists.Value)
                {
                    return BadRequest(new ApiResponse(400, "This Email Is Already Exist"));
                }

                // Create a new Student user
                var user = new Student
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Uid = registerDto.UId,
                    parent_number = registerDto.ParentNumber, // Store cleaned phone number
                    UserName = registerDto.Email.Split('@')[0],
                    EmailConfirmed = true
                };

                // Create the user with the specified password
                var createResult = await _studentManager.CreateAsync(user, registerDto.Password);
                if (!createResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Register failed"));
                }

                // Add the user to the "Student" role
                var roleResult = await _studentManager.AddToRoleAsync(user, "Student");
                if (!roleResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Failed to assign role"));
                }

                // Return the created user with the token
                var returnedUser = new StudentDto
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Uid = registerDto.UId,
                    Token = _tokenService.CreateTokenAsync(user)// Ensure the token creation is awaited
                };

                return Ok(Result<StudentDto>.Success(returnedUser, "Create successful"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, $"Internal server error: {ex.Message}"));
            }
        }

        [HttpGet("IsStudentExist")]
        public async Task<ActionResult<bool>> CheckIfStudentExist(string Email)
        {
            return await _studentManager.FindByEmailAsync(Email) is not null;
        }
    }
}