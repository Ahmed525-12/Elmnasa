using AutoMapper;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaApp.Specf.AppSpecf.AnswerSpecf;
using ElmnasaApp.Specf.AppSpecf.QuestionSpecf;
using ElmnasaApp.Specf.AppSpecf.SubscribeSubjectSpecf;

using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.QuestionDTO;
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
    public class QuestionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;

        public QuestionController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<Account> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionReadDto>>> GetAllQuestions()
        {
            try
            {
                var Questions = await _unitOfWork.Repository<Question>().GetAllWithAsync();
                var QuestionDtos = _mapper.Map<IEnumerable<QuestionReadDto>>(Questions);

                return Ok(Result<IEnumerable<QuestionReadDto>>.Success(QuestionDtos, "Get All Question successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<QuestionReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionReadDto>> GetQuestionById([FromQuery] int id)
        {
            try
            {
                var specf = new QuestionGetbyIdSpecf(id);
                var Question = await _unitOfWork.Repository<Question>().GetAllWithSpecAsync(specf);

                if (Question == null)
                {
                    // Return a NotFound response if the Question is not found
                    return NotFound(new ApiResponse(404, "Question not found."));
                }

                var QuestionDto = _mapper.Map<List<QuestionReadDto>>(Question);
                return Ok(Result<List<QuestionReadDto>>.Success(QuestionDto, "Get by id Question successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<QuestionReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<ActionResult<QuestionReadDto>> CreateQuestion([FromBody] QuestionCreateDto QuestionDto)
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);

                // Map QuestionDto to Question entity
                var question = _mapper.Map<Question>(QuestionDto);
                question.Teacher_id = user.Id;

                // Fetch the Answer entities using the AnswerIds from the DTO
                var answerIds = QuestionDto.AnswerIds.ToList();
                var answers = await _unitOfWork.Repository<Answer>()
                    .GetAllWithSpecAsync(new AnswerByIdSpecfication(answerIds));

                // Assign the fetched answers to the question
                question.Answers = answers.ToList(); // Convert to List or ICollection

                if (question.Answers == null || !question.Answers.Any())
                {
                    // Return a NotFound response if no Answers are found
                    return NotFound(new ApiResponse(404, "Answers not found."));
                }

                // Add and save the new question
                await _unitOfWork.Repository<Question>().AddAsync(question);
                await _unitOfWork.CompleteAsync();

                // Map the created question to the read DTO
                var createdQuestionDto = _mapper.Map<QuestionReadDto>(question);

                return Ok(Result<QuestionReadDto>.Success(createdQuestionDto, "Question created successfully"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up
                return Ok(Result<QuestionReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion([FromQuery] int id)
        {
            try
            {
                var Question = await _unitOfWork.Repository<Question>().GetbyIdAsync(id);
                if (Question == null)
                {
                    // Return a NotFound response if the Subject is not found
                    return NotFound(new ApiResponse(404, "Question not found."));
                }

                _unitOfWork.Repository<Question>().DeleteAsync(Question);
                await _unitOfWork.CompleteAsync();
                return Ok(Result<QuestionReadDto>.Success("Question Delete successfully"));
            }
            catch (Exception ex)
            {
                return Ok(Result<QuestionReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("GetQuestionsByTeacherId")]
        public async Task<ActionResult<IEnumerable<QuestionReadDto>>> GetQuestionsByTeacherId()
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);
                var spec = new QuestionByTeacherIdSpecification(user.Id);
                var Questions = await _unitOfWork.Repository<Question>().GetAllWithSpecAsync(spec);

                if (Questions == null || !Questions.Any())
                {
                    return NotFound(Result<IEnumerable<QuestionReadDto>>.Fail("No subscriptions found for the given Teacher ID."));
                }

                var QuestionDtos = _mapper.Map<IEnumerable<QuestionReadDto>>(Questions);
                return Ok(Result<IEnumerable<QuestionReadDto>>.Success(QuestionDtos, "Get Questions by TeacherId successful"));
            }
            catch (Exception ex)
            {
                return Ok(Result<QuestionReadDto>.Fail(ex.Message));
            }
        }
    }
}