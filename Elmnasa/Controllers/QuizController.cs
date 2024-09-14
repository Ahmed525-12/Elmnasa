using AutoMapper;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaApp.Specf.AppSpecf.AnswerSpecf;
using ElmnasaApp.Specf.AppSpecf.QuestionSpecf;
using ElmnasaApp.Specf.AppSpecf.QuizSpecf;
using ElmnasaApp.Specf.AppSpecf.SubscribeSubjectSpecf;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.QuizDTOs;
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
    public class QuizController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;

        public QuizController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<Account> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizReadDto>>> GetAllQuizs()
        {
            try
            {
                var Quizs = await _unitOfWork.Repository<Quiz>().GetAllWithAsync();
                var QuizDtos = _mapper.Map<IEnumerable<QuizReadDto>>(Quizs);

                return Ok(Result<IEnumerable<QuizReadDto>>.Success(QuizDtos, "Get All Quiz successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<QuizReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizReadDto>> GetQuizById([FromQuery] int id)
        {
            try
            {
                var specf = new QuizGetbyIdSpecf(id);
                var Quiz = await _unitOfWork.Repository<Quiz>().GetAllWithSpecAsync(specf);

                if (Quiz == null)
                {
                    // Return a NotFound response if the Quiz is not found
                    return NotFound(new ApiResponse(404, "Quiz not found."));
                }

                var QuizDto = _mapper.Map<List<QuizReadDto>>(Quiz);
                return Ok(Result<List<QuizReadDto>>.Success(QuizDto, "Get by id Quiz successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<QuizReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<ActionResult<QuizReadDto>> CreateQuiz([FromBody] QuizCreateDTO quizDto)
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);

                // Map QuizDto to Quiz entity
                var quiz = _mapper.Map<Quiz>(quizDto);
                quiz.Teacher_id = user.Id;

                // Ensure SubjectIds is a List<int>
                var subjectIds = quizDto.SubjectIds.ToList();

                // Fetch subjects by IDs
                var subjects = await _unitOfWork.Repository<TeacherSubject>().GetAllWithSpecAsync(new SubjectByIdsSpecification(subjectIds));

                // Check if subjects are found
                if (subjects == null || !subjects.Any())
                {
                    return NotFound(new ApiResponse(404, "Subject not found."));
                }

                // Convert IReadOnlyList<Subject> to ICollection<Subject>
                quiz.TeacherSubject = new List<TeacherSubject>(subjects);

                // Ensure QuestionIds is a List<int>
                var questionIds = quizDto.QuestionIds.ToList();

                // Fetch questions by IDs
                var questions = await _unitOfWork.Repository<Question>().GetAllWithSpecAsync(new QuestionGetbyIdSpecfForQuiz(questionIds));

                // Check if questions are found
                if (questions == null || !questions.Any())
                {
                    return NotFound(new ApiResponse(404, "Question not found."));
                }

                // Convert IReadOnlyList<Question> to ICollection<Question>
                quiz.Question = new List<Question>(questions);
                // Add and save the new Quiz
                await _unitOfWork.Repository<Quiz>().AddAsync(quiz);
                await _unitOfWork.CompleteAsync();

                // Map the created Quiz to the read DTO
                var createdQuizDto = _mapper.Map<QuizReadDto>(quiz);

                return Ok(Result<QuizReadDto>.Success(createdQuizDto, "Quiz created successfully"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up
                return Ok(Result<QuizReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz([FromQuery] int id)
        {
            try
            {
                var Quiz = await _unitOfWork.Repository<Quiz>().GetbyIdAsync(id);
                if (Quiz == null)
                {
                    // Return a NotFound response if the Subject is not found
                    return NotFound(new ApiResponse(404, "Quiz not found."));
                }

                _unitOfWork.Repository<Quiz>().DeleteAsync(Quiz);
                await _unitOfWork.CompleteAsync();
                return Ok(Result<QuizReadDto>.Success("Quiz Delete successfully"));
            }
            catch (Exception ex)
            {
                return Ok(Result<QuizReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("GetQuizsByTeacherId")]
        public async Task<ActionResult<IEnumerable<QuizReadDto>>> GetQuizsByTeacherId()
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);
                var spec = new QuizByTeacherIdSpecification(user.Id);
                var Quizs = await _unitOfWork.Repository<Quiz>().GetAllWithSpecAsync(spec);

                if (Quizs == null || !Quizs.Any())
                {
                    return NotFound(Result<IEnumerable<QuizReadDto>>.Fail("No subscriptions found for the given Teacher ID."));
                }

                var QuizDtos = _mapper.Map<IEnumerable<QuizReadDto>>(Quizs);
                return Ok(Result<IEnumerable<QuizReadDto>>.Success(QuizDtos, "Get Quizs by TeacherId successful"));
            }
            catch (Exception ex)

            {
                return Ok(Result<QuizReadDto>.Fail(ex.Message));
            }
        }
    }
}