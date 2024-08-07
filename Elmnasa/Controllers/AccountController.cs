using ElmnasaApp.Email.Intrefaces;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.JWTToken.Intrefaces;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.AdminDtos;
using ElmnasaDomain.DTOs.StudentDTOs;
using ElmnasaDomain.DTOs.TeacherDtos;
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
        public async Task<ActionResult<StudentDto>> StudentRegister(StudentReigsterDTO registerDto)
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

        [HttpPost("TeacherRegister")]
        public async Task<ActionResult<TeacherDto>> TeacherRegister(TeacherReigsterDTO registerDto)
        {
            try
            {
                // Check if the Teacher already exists
                var TeacherExists = await CheckIfTeacherExist(registerDto.Email);
                if (TeacherExists.Value)
                {
                    return BadRequest(new ApiResponse(400, "This Email Is Already Exist"));
                }

                // Create a new Teacher user
                var user = new Teacher
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Teacher_Image = registerDto.Teacher_Image,
                    UserName = registerDto.Email.Split('@')[0],
                    EmailConfirmed = true
                };

                // Create the user with the specified password
                var createResult = await _TeacherManager.CreateAsync(user, registerDto.Password);
                if (!createResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Register failed"));
                }

                // Add the user to the "Teacher" role
                var roleResult = await _TeacherManager.AddToRoleAsync(user, "Teacher");
                if (!roleResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Failed to assign role"));
                }

                // Return the created user with the token
                var returnedUser = new TeacherDto
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Token = _tokenService.CreateTokenAsync(user)// Ensure the token creation is awaited
                };

                return Ok(Result<TeacherDto>.Success(returnedUser, "Create successful"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, $"Internal server error: {ex.Message}"));
            }
        }

        [HttpPost("AdminRegister")]
        public async Task<ActionResult<AdminDto>> AdminRegister(AdminReigsterDTO registerDto)
        {
            try
            {
                // Check if the Admin already exists
                var AdminExists = await CheckIfAdminExist(registerDto.Email);
                if (AdminExists.Value)
                {
                    return BadRequest(new ApiResponse(400, "This Email Is Already Exist"));
                }

                // Create a new Admin user
                var user = new Admin
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,

                    UserName = registerDto.Email.Split('@')[0],
                    EmailConfirmed = true
                };

                // Create the user with the specified password
                var createResult = await _AdminManager.CreateAsync(user, registerDto.Password);
                if (!createResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Register failed"));
                }

                // Add the user to the "Admin" role
                var roleResult = await _AdminManager.AddToRoleAsync(user, "Admin");
                if (!roleResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Failed to assign role"));
                }

                // Return the created user with the token
                var returnedUser = new AdminDto
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Token = _tokenService.CreateTokenAsync(user)// Ensure the token creation is awaited
                };

                return Ok(Result<AdminDto>.Success(returnedUser, "Create successful"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse(500, $"Internal server error: {ex.Message}"));
            }
        }

        [HttpPost("StudentLogin")]
        public async Task<ActionResult<StudentDto>> StudentLogin(StudentDtoLogIn logInDto)
        {
            try
            {
                var user = await _studentManager.FindByEmailAsync(logInDto.Email);
                if (user == null)
                {
                    return Unauthorized(new ApiResponse(401, "User Not Found"));
                }

                if (!user.EmailConfirmed)
                {
                    return BadRequest(new ApiResponse(400, "Email not confirmed"));
                }

                if (await _studentManager.IsLockedOutAsync(user))
                {
                    return BadRequest(new ApiResponse(400, "User is locked out"));
                }

                var resultcode = await _signInManagerStudent.CheckPasswordSignInAsync(user, logInDto.Password, false);

                if (!resultcode.Succeeded)
                {
                    if (resultcode.IsLockedOut)
                        return BadRequest(new ApiResponse(400, "User is locked out"));
                    if (resultcode.IsNotAllowed)
                        return BadRequest(new ApiResponse(400, "User is not allowed to sign in"));
                    if (resultcode.RequiresTwoFactor)
                        return BadRequest(new ApiResponse(400, "Two-factor authentication required"));

                    return BadRequest(new ApiResponse(400, "Invalid login attempt"));
                }

                var returnedUser = new StudentDto
                {
                    Email = logInDto.Email,
                    Token = _tokenService.CreateTokenAsync(user)
                };

                return Ok((Result<StudentDto>.Success(returnedUser, "Create successful")));
            }
            catch (Exception ex)
            {
                return Ok(Result<StudentDto>.Fail(ex.Message));
            }
        }

        [HttpPost("TeacherLogin")]
        public async Task<ActionResult<TeacherDto>> TeacherLogin(TeacherDtoLogIn logInDto)
        {
            try
            {
                var user = await _TeacherManager.FindByEmailAsync(logInDto.Email);
                if (user == null)
                {
                    return Unauthorized(new ApiResponse(401, "User Not Found"));
                }

                if (!user.EmailConfirmed)
                {
                    return BadRequest(new ApiResponse(400, "Email not confirmed"));
                }

                if (await _TeacherManager.IsLockedOutAsync(user))
                {
                    return BadRequest(new ApiResponse(400, "User is locked out"));
                }

                var resultcode = await _signInManagerTeacher.CheckPasswordSignInAsync(user, logInDto.Password, false);

                if (!resultcode.Succeeded)
                {
                    if (resultcode.IsLockedOut)
                        return BadRequest(new ApiResponse(400, "User is locked out"));
                    if (resultcode.IsNotAllowed)
                        return BadRequest(new ApiResponse(400, "User is not allowed to sign in"));
                    if (resultcode.RequiresTwoFactor)
                        return BadRequest(new ApiResponse(400, "Two-factor authentication required"));

                    return BadRequest(new ApiResponse(400, "Invalid login attempt"));
                }

                var returnedUser = new TeacherDto
                {
                    Email = logInDto.Email,
                    Token = _tokenService.CreateTokenAsync(user)
                };

                return Ok((Result<TeacherDto>.Success(returnedUser, "Create successful")));
            }
            catch (Exception ex)
            {
                return Ok(Result<TeacherDto>.Fail(ex.Message));
            }
        }

        [HttpPost("AdminLogin")]
        public async Task<ActionResult<AdminDto>> AdminLogin(AdminDtoLogIn logInDto)
        {
            try
            {
                var user = await _AdminManager.FindByEmailAsync(logInDto.Email);
                if (user == null)
                {
                    return Unauthorized(new ApiResponse(401, "User Not Found"));
                }

                if (!user.EmailConfirmed)
                {
                    return BadRequest(new ApiResponse(400, "Email not confirmed"));
                }

                if (await _AdminManager.IsLockedOutAsync(user))
                {
                    return BadRequest(new ApiResponse(400, "User is locked out"));
                }

                var resultcode = await _signInManagerAdmin.CheckPasswordSignInAsync(user, logInDto.Password, false);

                if (!resultcode.Succeeded)
                {
                    if (resultcode.IsLockedOut)
                        return BadRequest(new ApiResponse(400, "User is locked out"));
                    if (resultcode.IsNotAllowed)
                        return BadRequest(new ApiResponse(400, "User is not allowed to sign in"));
                    if (resultcode.RequiresTwoFactor)
                        return BadRequest(new ApiResponse(400, "Two-factor authentication required"));

                    return BadRequest(new ApiResponse(400, "Invalid login attempt"));
                }

                var returnedUser = new AdminDto
                {
                    Email = logInDto.Email,
                    Token = _tokenService.CreateTokenAsync(user)
                };

                return Ok((Result<AdminDto>.Success(returnedUser, "Create successful")));
            }
            catch (Exception ex)
            {
                return Ok(Result<AdminDto>.Fail(ex.Message));
            }
        }

        [HttpGet("IsStudentExist")]
        public async Task<ActionResult<bool>> CheckIfStudentExist(string Email)
        {
            return await _studentManager.FindByEmailAsync(Email) is not null;
        }

        [HttpGet("IsTeacherExist")]
        public async Task<ActionResult<bool>> CheckIfTeacherExist(string Email)
        {
            return await _TeacherManager.FindByEmailAsync(Email) is not null;
        }

        [HttpGet("IsAdminExist")]
        public async Task<ActionResult<bool>> CheckIfAdminExist(string Email)
        {
            return await _AdminManager.FindByEmailAsync(Email) is not null;
        }
    }
}