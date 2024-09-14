using AutoMapper;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaApp.Specf.AppSpecf.AnswerSpecf;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.AnswerDtos;

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
    public class AnswerController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;

        public AnswerController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<Account> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost("CreateAnswer")]
        public async Task<ActionResult<AnswerDTO>> CreateAnswer([FromBody] AnswerDTO model)
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

                // Create a new Answer entity using data from the model and user
                var Answer = new Answer
                {
                    isTrue = model.isTrue,
                    Name = model.Name,
                    Teacher_id = user.Id,
                };

                // Add the new Answer entity to the repository and save changes
                await _unitOfWork.Repository<Answer>().AddAsync(Answer);
                await _unitOfWork.CompleteAsync();

                // Return a success response with the model data
                return Ok(Result<Answer>.Success(Answer, "Create successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<AnswerDTO>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("UpdateAnswer")]
        public async Task<ActionResult<AnswerDTO>> UpdateAnswer([FromBody] UpdateAnswerDto model)
        {
            try
            {
                // Validate the model to ensure it's not null
                if (model == null)
                {
                    // Return a BadRequest response if the model is null
                    return BadRequest(new ApiResponse(400, "Invalid request data."));
                }

                // Map the UpdateAnswerDTO to a Answer entity
                var mapped = _mapper.Map<Answer>(model);

                // Validate the mapping result
                if (mapped == null)
                {
                    // Return a BadRequest response if mapping failed
                    return BadRequest(new ApiResponse(400, "Failed to map UpdateAnswerDTO to Answer."));
                }

                // Update the Grades entity in the repository asynchronously
                _unitOfWork.Repository<Answer>().Update(mapped);
                await _unitOfWork.CompleteAsync();

                // Return a success response with the updated data
                return Ok(Result<Answer>.Success(mapped, "Update successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<AnswerDTO>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDTO>> GetAnswerById([FromQuery] int id)
        {
            try
            {
                // Retrieve the Answer entity from the repository by ID
                var Answer = await _unitOfWork.Repository<Answer>().GetbyIdAsync(id);

                // Check if the Answer entity was found
                if (Answer is null)
                {
                    // Return a NotFound response if the Answer is not found
                    return NotFound(new ApiResponse(404, "Answer not found."));
                }

                // Map the retrieved Answers entity to a AnswerDto
                var mappedItem = _mapper.Map<AnswerDTO>(Answer);

                // Return a success response with the mapped data
                return Ok(Result<AnswerDTO>.Success(mappedItem, "Get successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<AnswerDTO>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer([FromQuery] int id)
        {
            try
            {
                // Retrieve the Answer entity from the repository by ID
                var Answer = await _unitOfWork.Repository<Answer>().GetbyIdAsync(id);

                // Check if the Answer entity was found
                if (Answer == null)
                {
                    // Return a NotFound response if the Answer is not found
                    return NotFound(new ApiResponse(404, "Answer not found"));
                }

                // Delete the Answer entity from the repository asynchronously
                _unitOfWork.Repository<Answer>().DeleteAsync(Answer);

                // Save changes to the database
                await _unitOfWork.CompleteAsync();

                // Return a success response indicating deletion was successful
                return Ok(Result.Success("Delete successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<AnswerDTO>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerDTO>>> GetAllAnswers()
        {
            try
            {
                // Retrieve all Answer entities from the repository asynchronously
                var Answers = await _unitOfWork.Repository<Answer>().GetAllWithAsync();

                // Return a success response with the mapped list
                return Ok(Result<IEnumerable<Answer>>.Success(Answers, "Get all Answers successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<IEnumerable<AnswerDTO>>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("GetAllUserAnswers")]
        public async Task<ActionResult<IEnumerable<AnswerDTO>>> GetAllUserAnswers()
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);

                // Create a specification for retrieving the user's Answers
                var spec = new UserAnswerSpecf(user.Id);

                // Retrieve the Answers for the specified user based on the specification
                var userAnswers = await _unitOfWork.Repository<Answer>().GetAllWithSpecAsync(spec);

                // Map the list of Answers entities to a list of AnswerDto
                var mappedResult = _mapper.Map<List<AnswerDTO>>(userAnswers);

                // Return a success response with the mapped list
                return Ok(Result<IEnumerable<AnswerDTO>>.Success(mappedResult, "Get all user Answers successful"));
            }
            catch (Exception ex)
            {
                // Return a failure response with the exception message
                return Ok(Result<AnswerDTO>.Fail(ex.Message));
            }
        }
    }
}