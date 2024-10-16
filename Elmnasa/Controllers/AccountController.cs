﻿using ElmnasaApp.Email.Intrefaces;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.JWTToken.Intrefaces;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.Account;
using ElmnasaDomain.DTOs.AdminDtos;
using ElmnasaDomain.DTOs.EmailDTO;
using ElmnasaDomain.DTOs.StudentDTOs;
using ElmnasaDomain.DTOs.TeacherDtos;
using ElmnasaDomain.Entites.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Elmnasa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IEmailSettings _emailSettings;

        private readonly UserManager<Student> _studentManager;

        private readonly UserManager<Teacher> _TeacherManager;

        private readonly UserManager<Admin> _AdminManager;
        private readonly SignInManager<Admin> _signInManagerAdmin;
        private readonly UserManager<Account> _accountManager;
        private readonly SignInManager<Account> _signInManagerAccount;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<Student> studentManager,

        UserManager<Teacher> TeacherManager,

         UserManager<Admin> AdminManager,
        SignInManager<Admin> signInManagerAdmin,
           UserManager<Account> AccountManager,
        SignInManager<Account> signInManagerAccount,
        ITokenService tokenService,

            IEmailSettings emailSettings

            )
        {
            _studentManager = studentManager;

            _TeacherManager = TeacherManager;

            _AdminManager = AdminManager;
            _signInManagerAdmin = signInManagerAdmin;
            _accountManager = AccountManager;
            _signInManagerAccount = signInManagerAccount;
            _tokenService = tokenService;

            _emailSettings = emailSettings;
        }

        [HttpPost("StudentRegister")]
        public async Task<ActionResult<StudentDto>> StudentRegister([FromBody] StudentReigsterDTO registerDto)
        {
            try
            {
                // Step 1: Validate the incoming model state
                // If the model state is invalid (e.g., missing or incorrect data), return a BadRequest with validation errors.
                if (!ModelState.IsValid)
                {
                    // Extract the error messages from ModelState
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    // Convert the list of errors into a single string or pass the list itself if your Fail method supports it
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result<StudentDto>.Fail(errorMessage));
                }

                // Check if the guest already exists
                var guestExists = await CheckIfStudentExist(registerDto.Email);
                if (guestExists.Value)
                {
                    return BadRequest(new ApiResponse(400, "This Email Is Already Exist"));
                }

                // Step 3: Create a new Student object using the data from the DTO
                var user = new Student
                {
                    DisplayName = registerDto.DisplayName, // Set the display name
                    Email = registerDto.Email, // Set the email
                    Uid = registerDto.UId, // Set the unique identifier
                    parent_number = registerDto.ParentNumber, // Store the parent's phone number
                    UserName = registerDto.Email.Split('@')[0], // Use part of the email as the username
                    EmailConfirmed = true // Set email as confirmed
                };

                // Step 4: Create the student user in the system with the provided password
                // If user creation fails, return a BadRequest with an appropriate error message.
                var createResult = await _studentManager.CreateAsync(user, registerDto.Password);
                if (!createResult.Succeeded)
                {
                    var errors = createResult.Errors.Select(e => e.Description).ToList();

                    // Convert the list of errors into a single string or join them with a separator
                    var errorMessage = string.Join("; ", errors);
                    return Ok(Result<StudentDto>.Fail(errorMessage));
                }

                // Step 5: Assign the "Student" role to the newly created user
                // If assigning the role fails, return a BadRequest with an appropriate error message.
                var roleResult = await _studentManager.AddToRoleAsync(user, "Student");
                if (!roleResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Failed to assign role"));
                }

                // Step 6: Create a DTO for the response that includes a token for authentication
                var returnedUser = new StudentDto
                {
                    DisplayName = registerDto.DisplayName, // Set the display name
                    Email = registerDto.Email, // Set the email
                    Uid = registerDto.UId, // Set the unique identifier
                    Token = await _tokenService.CreateToken(user) // Generate a token for the user
                };

                // Step 7: Return a success result with the created user data
                return Ok(Result<StudentDto>.Success(returnedUser, "Create successful"));
            }
            catch (Exception ex)
            {
                // Step 8: Handle any exceptions that occur during the process
                // Return a failure result with the exception message.
                return Ok(Result<StudentDto>.Fail(ex.Message));
            }
        }

        [HttpPost("TeacherRegister")]
        public async Task<ActionResult<TeacherDto>> TeacherRegister([FromBody] TeacherReigsterDTO registerDto)
        {
            try
            {
                // Step 1: Validate the incoming model state
                if (!ModelState.IsValid)
                {
                    // Extract the error messages from ModelState
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    // Convert the list of errors into a single string or pass the list itself if your Fail method supports it
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result<TeacherDto>.Fail(errorMessage));
                }

                // Step 2: Check if a teacher with the provided email already exists
                // Check if the guest already exists
                var guestExists = await CheckIfTeacherExist(registerDto.Email);
                if (guestExists.Value)
                {
                    return BadRequest(new ApiResponse(400, "This Email Is Already Exist"));
                }

                // Step 3: Create a new Teacher object using the data from the DTO
                var user = new Teacher
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Teacher_Image = registerDto.Teacher_Image,
                    UserName = registerDto.Email.Split('@')[0],
                    EmailConfirmed = true
                };

                // Step 4: Create the teacher user in the system with the provided password
                var createResult = await _TeacherManager.CreateAsync(user, registerDto.Password);
                if (!createResult.Succeeded)
                {
                    var errors = createResult.Errors.Select(e => e.Description).ToList();

                    // Convert the list of errors into a single string or join them with a separator
                    var errorMessage = string.Join("; ", errors);
                    return Ok(Result<TeacherDto>.Fail(errorMessage));
                }

                // Step 5: Assign the "Teacher" role to the newly created user
                var roleResult = await _TeacherManager.AddToRoleAsync(user, "Teacher");
                if (!roleResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Failed to assign role"));
                }

                // Step 6: Prepare the response with the created user's information and token
                var returnedUser = new TeacherDto
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Token = await _tokenService.CreateToken(user) // Ensure the token creation is awaited
                };

                // Step 7: Return a success result with the created user data
                return Ok(Result<TeacherDto>.Success(returnedUser, "Create successful"));
            }
            catch (Exception ex)
            {
                // Step 8: Handle any exceptions that occur during the process
                // Return a failure result with the exception message.
                return Ok(Result<TeacherDto>.Fail(ex.Message));
            }
        }

        [HttpPost("AdminRegister")]
        public async Task<ActionResult<AdminDto>> AdminRegister([FromBody] AdminReigsterDTO registerDto)
        {
            try
            {
                // Step 1: Validate the incoming model state
                if (!ModelState.IsValid)
                {
                    // Extract the error messages from ModelState
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    // Convert the list of errors into a single string or pass the list itself if your Fail method supports it
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result<AdminDto>.Fail(errorMessage));
                }

                // Step 2: Check if an admin with the provided email already exists
                // Check if the guest already exists
                var guestExists = await CheckIfAdminExist(registerDto.Email);
                if (guestExists.Value)
                {
                    return BadRequest(new ApiResponse(400, "This Email Is Already Exist"));
                }

                // Step 3: Create a new Admin object using the data from the DTO
                var user = new Admin
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    UserName = registerDto.Email.Split('@')[0],
                    EmailConfirmed = true
                };

                // Step 4: Create the admin user in the system with the provided password
                var createResult = await _AdminManager.CreateAsync(user, registerDto.Password);
                if (!createResult.Succeeded)
                {
                    var errors = createResult.Errors.Select(e => e.Description).ToList();

                    // Convert the list of errors into a single string or join them with a separator
                    var errorMessage = string.Join("; ", errors);
                    return Ok(Result<AdminDto>.Fail(errorMessage));
                }

                // Step 5: Assign the "Admin" role to the newly created user
                var roleResult = await _AdminManager.AddToRoleAsync(user, "Admin");
                if (!roleResult.Succeeded)
                {
                    return BadRequest(new ApiResponse(400, "Failed to assign role"));
                }

                // Step 6: Prepare the response with the created user's information and token
                var returnedUser = new AdminDto
                {
                    DisplayName = registerDto.DisplayName,
                    Email = registerDto.Email,
                    Token = await _tokenService.CreateToken(user) // Ensure the token creation is awaited
                };

                // Step 7: Return a success result with the created user data
                return Ok(Result<AdminDto>.Success(returnedUser, "Create successful"));
            }
            catch (Exception ex)
            {
                // Step 8: Handle any exceptions that occur during the process
                // Return a failure result with the exception message.
                return Ok(Result<AdminDto>.Fail(ex.Message));
            }
        }

        [HttpPost("AccountLogin")]
        public async Task<ActionResult<AccountDTO>> AccountLogin([FromBody] StudentDtoLogIn logInDto)
        {
            try
            {
                // Step 1: Validate the incoming model state
                if (!ModelState.IsValid)
                {
                    // Extract the error messages from ModelState
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    // Convert the list of errors into a single string or pass the list itself if your Fail method supports it
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result<StudentDto>.Fail(errorMessage));
                }

                // Step 2: Find the user by email
                var user = await _accountManager.FindByEmailAsync(logInDto.Email);
                if (user == null)
                {
                    // If the user is not found, return an Unauthorized response
                    return Unauthorized(new ApiResponse(401, "User Not Found"));
                }

                // Step 3: Check if the email is confirmed
                if (!user.EmailConfirmed)
                {
                    // If the email is not confirmed, return a BadRequest response
                    return BadRequest(new ApiResponse(400, "Email not confirmed"));
                }

                // Step 4: Check if the user is locked out
                if (await _accountManager.IsLockedOutAsync(user))
                {
                    // If the user is locked out, return a BadRequest response
                    return BadRequest(new ApiResponse(400, "User is locked out"));
                }

                // Step 5: Check the user's password
                var resultcode = await _signInManagerAccount.CheckPasswordSignInAsync(user, logInDto.Password, false);
                var roleName = await _accountManager.GetRolesAsync(user);
                if (!resultcode.Succeeded)
                {
                    // If the password check fails, return appropriate error messages
                    if (resultcode.IsLockedOut)
                        return BadRequest(new ApiResponse(400, "User is locked out"));
                    if (resultcode.IsNotAllowed)
                        return BadRequest(new ApiResponse(400, "User is not allowed to sign in"));
                    if (resultcode.RequiresTwoFactor)
                        return BadRequest(new ApiResponse(400, "Two-factor authentication required"));

                    // If none of the above, return a generic invalid login attempt message
                    return BadRequest(new ApiResponse(400, "Password or email is wrong"));
                }

                // Step 6: Create a response object with the user's email and a token
                var returnedUser = new AccountDTO
                {
                    Email = logInDto.Email,
                    Token = await _tokenService.CreateToken(user),
                    DisplayName = user.DisplayName,
                    RoleName = (List<string>)roleName
                };

                // Step 7: Return a success response with the created token
                return Ok(Result<AccountDTO>.Success(returnedUser, "Login successful"));
            }
            catch (Exception ex)
            {
                // Step 8: Handle any exceptions that occur during the process
                return StatusCode(500, Result<AccountDTO>.Fail(ex.Message));
            }
        }

        [HttpPost("AdminLogin")]
        public async Task<ActionResult<AdminDto>> AdminLogin([FromBody] AdminDtoLogIn logInDto)
        {
            try
            {
                // Step 1: Validate the incoming model state
                if (!ModelState.IsValid)
                {
                    // Extract the error messages from ModelState
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    // Convert the list of errors into a single string or pass the list itself if your Fail method supports it
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result<AdminDto>.Fail(errorMessage));
                }

                // Step 2: Find the user by email
                var user = await _AdminManager.FindByEmailAsync(logInDto.Email);
                if (user == null)
                {
                    // If the user is not found, return an Unauthorized response
                    return Unauthorized(new ApiResponse(401, "User Not Found"));
                }

                // Step 3: Check if the email is confirmed
                if (!user.EmailConfirmed)
                {
                    // If the email is not confirmed, return a BadRequest response
                    return BadRequest(new ApiResponse(400, "Email not confirmed"));
                }

                // Step 4: Check if the user is locked out
                if (await _AdminManager.IsLockedOutAsync(user))
                {
                    // If the user is locked out, return a BadRequest response
                    return BadRequest(new ApiResponse(400, "User is locked out"));
                }

                // Step 5: Check the user's password
                var resultcode = await _signInManagerAdmin.CheckPasswordSignInAsync(user, logInDto.Password, false);

                if (!resultcode.Succeeded)
                {
                    // If the password check fails, return appropriate error messages
                    if (resultcode.IsLockedOut)
                        return BadRequest(new ApiResponse(400, "User is locked out"));
                    if (resultcode.IsNotAllowed)
                        return BadRequest(new ApiResponse(400, "User is not allowed to sign in"));
                    if (resultcode.RequiresTwoFactor)
                        return BadRequest(new ApiResponse(400, "Two-factor authentication required"));

                    // If none of the above, return a generic invalid login attempt message
                    return BadRequest(new ApiResponse(400, "Password or email is wrong"));
                }

                // Step 6: Create a response object with the user's email and a token
                var returnedUser = new AdminDto
                {
                    Email = logInDto.Email,
                    Token = await _tokenService.CreateToken(user)
                };

                // Step 7: Return a success response with the created token
                return Ok(Result<AdminDto>.Success(returnedUser, "Login successful"));
            }
            catch (Exception ex)
            {
                // Step 8: Handle any exceptions that occur during the process
                return StatusCode(500, Result<AdminDto>.Fail(ex.Message));
            }
        }

        [HttpPost("SendEmailAccount")]
        public async Task<IActionResult> SendEmailAccount([FromBody] ForgetPasswordDto emailinput)
        {
            try
            {
                // Step 1: Validate the input model state
                if (!ModelState.IsValid)
                {
                    // Extract error messages from ModelState
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result.Fail(errorMessage));
                }
                // Step 1: Validate the input email
                if (string.IsNullOrEmpty(emailinput.Email))
                {
                    return BadRequest("Email input cannot be null or empty.");
                }

                // Step 2: Find the student by email
                var user = await _accountManager.FindByEmailAsync(emailinput.Email);
                if (user == null)
                {
                    // If the user is not found, return a failure response
                    return Ok(Result.Fail("Email does not exist."));
                }

                // Step 3: Generate a password reset token
                var token = await _tokenService.CreateToken(user);
                var resetPasswordLink = $"{Request.Scheme}://{Request.Host}/Account/ResetPassword?email={user.Email}&Token={token}";

                // Step 4: Create the email object
                var email = new EmailDTO
                {
                    Subject = "Reset Password",
                    To = emailinput.Email,
                    Body = $"Please reset your password by clicking the following link: {resetPasswordLink}",
                };

                // Step 5: Send the email
                _emailSettings.SendEmail(email);

                // Step 6: Create a response object with the token and email
                var emailSuccess = new RecieveEmail
                {
                    token = token,
                    email = emailinput.Email,
                };

                // Step 7: Return a success response
                return Ok(Result<RecieveEmail>.Success(emailSuccess, "Success"));
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return a failure response
                return StatusCode(500, Result.Fail($"An error occurred: {ex.Message}"));
            }
        }

        [HttpPost("ResetPasswordAccount")]
        public async Task<IActionResult> ResetPasswordAccount([FromBody] ResetPasswordDto model)
        {
            try
            {
                // Step 1: Validate the input model state
                if (!ModelState.IsValid)
                {
                    // Extract error messages from ModelState
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result.Fail(errorMessage));
                }

                // Step 2: Find the user by email
                var user = await _accountManager.FindByEmailAsync(model.email);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                // Step 3: Reset the user's password
                var result = await _accountManager.ResetPasswordAsync(user, model.token, model.Password);

                if (result.Succeeded)
                {
                    // Step 4: Return a success response
                    return Ok(Result.Success("Password reset successful."));
                }
                else
                {
                    // If password reset fails, return a failure response with the error details
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result.Fail($"Failed to reset password: {errorMessage}"));
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the process
                return StatusCode(500, Result.Fail($"An error occurred: {ex.Message}"));
            }
        }

        [HttpPost("ChangePasswordAccount")]
        public async Task<IActionResult> ChangePasswordAccount([FromBody] ChangePasswordTeacherDTO model)
        {
            try
            {
                // Step 1: Validate the input model state
                if (!ModelState.IsValid)
                {
                    // Extract error messages from ModelState
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result.Fail(errorMessage));
                }

                // Step 2: Validate the input fields
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.CurrentPassword) || string.IsNullOrEmpty(model.NewPassword))
                {
                    return BadRequest("Invalid input. All fields are required.");
                }

                // Step 3: Find the student by email
                var user = await _accountManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                // Step 4: Change the student's password
                var result = await _accountManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    // Step 5: Return a success response
                    return Ok(Result.Success("Password changed successfully."));
                }
                else
                {
                    // Step 6: If the password change fails, return the error details
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result.Fail($"Failed to change password: {errorMessage}"));
                }
            }
            catch (Exception ex)
            {
                // Step 7: Handle any exceptions and return a failure response
                return StatusCode(500, Result.Fail($"An error occurred: {ex.Message}"));
            }
        }

        [HttpGet("IsStudentExist")]
        public async Task<ActionResult<bool>> CheckIfStudentExist([FromQuery] string email)
        {
            try
            {
                // Step 1: Validate the input model state
                if (!ModelState.IsValid)
                {
                    // Extract error messages from ModelState
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result.Fail(errorMessage));
                }
                // Validate the email parameter
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest(Result.Fail("Email parameter cannot be null or empty."));
                }

                // Check if the student exists
                var user = await _studentManager.FindByEmailAsync(email);
                if (user != null)
                {
                    return Ok(Result<bool>.Success(true, "Student exists"));
                }
                else
                {
                    return Ok(Result.Fail("Student does not exist"));
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(500, Result<bool>.Fail($"An error occurred: {ex.Message}"));
            }
        }

        [HttpGet("IsTeacherExist")]
        public async Task<ActionResult<bool>> CheckIfTeacherExist([FromQuery] string email)
        {
            try
            {
                // Step 1: Validate the input model state
                if (!ModelState.IsValid)
                {
                    // Extract error messages from ModelState
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result.Fail(errorMessage));
                }
                // Validate the email parameter
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest(Result.Fail("Email parameter cannot be null or empty."));
                }

                // Check if the teacher exists
                var user = await _TeacherManager.FindByEmailAsync(email);
                if (user != null)
                {
                    return Ok(Result<bool>.Success(true, "Teacher exists"));
                }
                else
                {
                    return Ok(Result.Fail("Teacher does not exist"));
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(500, Result<bool>.Fail($"An error occurred: {ex.Message}"));
            }
        }

        [HttpGet("IsAdminExist")]
        public async Task<ActionResult<bool>> CheckIfAdminExist([FromQuery] string email)
        {
            try
            {
                // Step 1: Validate the input model state
                if (!ModelState.IsValid)
                {
                    // Extract error messages from ModelState
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                   .Select(e => e.ErrorMessage)
                                                   .ToList();
                    var errorMessage = string.Join("; ", errors);
                    return BadRequest(Result.Fail(errorMessage));
                }
                // Validate the email parameter
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest(Result.Fail("Email parameter cannot be null or empty."));
                }

                // Check if the admin exists
                var user = await _AdminManager.FindByEmailAsync(email);
                if (user != null)
                {
                    return Ok(Result<bool>.Success(true, "Admin exists"));
                }
                else
                {
                    return Ok(Result.Fail("Admin does not exist"));
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(500, Result<bool>.Fail($"An error occurred: {ex.Message}"));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("VerfiedTeacher")]
        public async Task<IActionResult> VerfiedTeacher(string Email)
        {
            try
            {
                var user = await _TeacherManager.FindByEmailAsync(Email);
                user.IsVerfied = true;
                await _TeacherManager.UpdateAsync(user);
                return Ok(Result.Success("teacher is Verfied"));
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(500, Result.Fail($"An error occurred: {ex.Message}"));
            }
        }
    }
}