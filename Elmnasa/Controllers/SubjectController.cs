using AutoMapper;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaApp.Specf.AppSpecf.GradesSpecf;
using ElmnasaApp.Specf.AppSpecf.SubjectSpecf;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.GradesDTO;
using ElmnasaDomain.DTOs.SubjectDTOS;
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
    public class SubjectController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;

        public SubjectController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<Account> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateSubject")]
        public async Task<ActionResult<SubjectDTO>> CreateSubject([FromBody] SubjectDTO model)
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

                // Create a new Subject entity using data from the model and user
                var Subject = new Subject
                {
                    Subject_Name = model.Subject_Name,
                    Account_id = user.Id,
                };

                // Add the new Subject entity to the repository and save changes
                await _unitOfWork.Repository<Subject>().AddAsync(Subject);
                await _unitOfWork.CompleteAsync();

                // Return a success response with the model data
                return Ok(Result<Subject>.Success(Subject, "Create successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<SubjectDTO>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("UpdateSubject")]
        public async Task<ActionResult<SubjectDTO>> UpdateSubject([FromBody] UpdateSubjectDto model)
        {
            try
            {
                // Validate the model to ensure it's not null
                if (model == null)
                {
                    // Return a BadRequest response if the model is null
                    return BadRequest(new ApiResponse(400, "Invalid request data."));
                }

                // Map the UpdateSubjectDTO to a Subject entity
                var mapped = _mapper.Map<Subject>(model);

                // Validate the mapping result
                if (mapped == null)
                {
                    // Return a BadRequest response if mapping failed
                    return BadRequest(new ApiResponse(400, "Failed to map UpdateSubjectDTO to Subject."));
                }

                // Update the Grades entity in the repository asynchronously
                _unitOfWork.Repository<Subject>().Update(mapped);
                await _unitOfWork.CompleteAsync();

                // Return a success response with the updated data
                return Ok(Result<Subject>.Success(mapped, "Update successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<SubjectDTO>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDTO>> GetSubjectById([FromQuery] int id)
        {
            try
            {
                // Retrieve the Subject entity from the repository by ID
                var Subject = await _unitOfWork.Repository<Subject>().GetbyIdAsync(id);

                // Check if the Subject entity was found
                if (Subject is null)
                {
                    // Return a NotFound response if the Subject is not found
                    return NotFound(new ApiResponse(404, "Subject not found."));
                }

                // Map the retrieved Subjects entity to a SubjectDto
                var mappedItem = _mapper.Map<SubjectDTO>(Subject);

                // Return a success response with the mapped data
                return Ok(Result<SubjectDTO>.Success(mappedItem, "Get successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<SubjectDTO>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject([FromQuery] int id)
        {
            try
            {
                // Retrieve the Subject entity from the repository by ID
                var Subject = await _unitOfWork.Repository<Subject>().GetbyIdAsync(id);

                // Check if the Subject entity was found
                if (Subject == null)
                {
                    // Return a NotFound response if the Subject is not found
                    return NotFound(new ApiResponse(404, "Subject not found"));
                }

                // Delete the Subject entity from the repository asynchronously
                _unitOfWork.Repository<Subject>().DeleteAsync(Subject);

                // Save changes to the database
                await _unitOfWork.CompleteAsync();

                // Return a success response indicating deletion was successful
                return Ok(Result.Success("Delete successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<SubjectDTO>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Student,Teacher")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetAllSubjects()
        {
            try
            {
                // Retrieve all Subject entities from the repository asynchronously
                var Subjects = await _unitOfWork.Repository<Subject>().GetAllWithAsync();

                // Return a success response with the mapped list
                return Ok(Result<IEnumerable<Subject>>.Success(Subjects, "Get all Subjects successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<IEnumerable<SubjectDTO>>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUserSubjects")]
        public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetAllUserSubjects()
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);

                // Create a specification for retrieving the user's Subjects
                var spec = new UserSubjectSpecf(user.Id);

                // Retrieve the Subjects for the specified user based on the specification
                var userSubjects = await _unitOfWork.Repository<Subject>().GetAllWithSpecAsync(spec);

                // Map the list of Subjects entities to a list of SubjectDto
                var mappedResult = _mapper.Map<List<SubjectDTO>>(userSubjects);

                // Return a success response with the mapped list
                return Ok(Result<IEnumerable<SubjectDTO>>.Success(mappedResult, "Get all user Subjects successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<SubjectDTO>.Fail(ex.Message));
            }
        }
    }
}