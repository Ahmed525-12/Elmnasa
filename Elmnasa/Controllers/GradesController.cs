using AutoMapper;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaApp.Specf.AppSpecf.GradesSpecf;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.GradesDTO;
using ElmnasaDomain.DTOs.StudentDTOs;
using ElmnasaDomain.Entites.app;
using ElmnasaDomain.Entites.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Elmnasa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;

        public GradesController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<Account> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = "Student,Admin")]
        [HttpPost("CreateGrade")]
        public async Task<ActionResult<GradeDto>> CreateGrade([FromBody] GradeDto model)
        {
            try
            {
                // Validate the model to ensure it's not null
                if (model == null)
                {
                    // Return a BadRequest response if the model is null
                    return BadRequest(new ApiResponse(400, "Invalid request data."));
                }

                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);

                // Create a new Grades entity using data from the model and user
                var grade = new Grades
                {
                    Name = model.Name,
                    Student_id = user.Id
                };

                // Add the new grade entity to the repository and save changes
                await _unitOfWork.Repository<Grades>().AddAsync(grade);
                await _unitOfWork.CompleteAsync();

                // Return a success response with the model data
                return Ok(Result<GradeDto>.Success(model, "Create successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<GradeDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateGrade")]
        public async Task<ActionResult<GradeDto>> UpdateGrade([FromBody] UpdateGradeDTO model)
        {
            try
            {
                // Validate the model to ensure it's not null
                if (model == null)
                {
                    // Return a BadRequest response if the model is null
                    return BadRequest(new ApiResponse(400, "Invalid request data."));
                }

                // Map the UpdateGradeDTO to a Grades entity
                var mapped = _mapper.Map<Grades>(model);

                // Validate the mapping result
                if (mapped == null)
                {
                    // Return a BadRequest response if mapping failed
                    return BadRequest(new ApiResponse(400, "Failed to map UpdateGradeDTO to Grades."));
                }

                // Update the Grades entity in the repository asynchronously
                _unitOfWork.Repository<Grades>().Update(mapped);
                await _unitOfWork.CompleteAsync();

                // Return a success response with the updated data
                return Ok(Result<Grades>.Success(mapped, "Update successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<GradeDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GradeDto>> GetGradeById([FromQuery] int id)
        {
            try
            {
                // Retrieve the grade entity from the repository by ID
                var grade = await _unitOfWork.Repository<Grades>().GetbyIdAsync(id);

                // Check if the grade entity was found
                if (grade is null)
                {
                    // Return a NotFound response if the grade is not found
                    return NotFound(new ApiResponse(404, "Grade not found."));
                }

                // Map the retrieved Grades entity to a GradeDto
                var mappedItem = _mapper.Map<GradeDto>(grade);

                // Return a success response with the mapped data
                return Ok(Result<GradeDto>.Success(mappedItem, "Get successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<GradeDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade([FromQuery] int id)
        {
            try
            {
                // Retrieve the grade entity from the repository by ID
                var grade = await _unitOfWork.Repository<Grades>().GetbyIdAsync(id);

                // Check if the grade entity was found
                if (grade == null)
                {
                    // Return a NotFound response if the grade is not found
                    return NotFound(new ApiResponse(404, "Grade not found"));
                }

                // Delete the grade entity from the repository asynchronously
                _unitOfWork.Repository<Grades>().DeleteAsync(grade);

                // Save changes to the database
                await _unitOfWork.CompleteAsync();

                // Return a success response indicating deletion was successful
                return Ok(Result.Success("Delete successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<GradeDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetAllGrades()
        {
            try
            {
                // Retrieve all grade entities from the repository asynchronously
                var grades = await _unitOfWork.Repository<Grades>().GetAllWithAsync();

                // If the repository returns null or an empty list, you might want to handle that scenario

                // Map the list of Grades entities to a list of GradeDto
                var mappedGrades = _mapper.Map<IEnumerable<GradeDto>>(grades);

                // Return a success response with the mapped list
                return Ok(Result<IEnumerable<GradeDto>>.Success(mappedGrades, "Get all grades successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<IEnumerable<GradeDto>>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("GetAllUserGrades")]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetAllUserGrades()
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);

                // Create a specification for retrieving the user's grades
                var spec = new UserGradeSpecf(user.Id);

                // Retrieve the grades for the specified user based on the specification
                var userGrades = await _unitOfWork.Repository<Grades>().GetAllWithSpecAsync(spec);

                // Map the list of Grades entities to a list of GradeDto
                var mappedResult = _mapper.Map<List<GradeDto>>(userGrades);

                // Return a success response with the mapped list
                return Ok(Result<IEnumerable<GradeDto>>.Success(mappedResult, "Get all user grades successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<GradeDto>.Fail(ex.Message));
            }
        }
    }
}