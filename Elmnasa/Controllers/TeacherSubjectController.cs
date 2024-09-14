using AutoMapper;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaApp.Specf.AppSpecf.TeacherSubjectsSpecf;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.TeacherSubjectDTOS;
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
    public class TeacherSubjectController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;

        public TeacherSubjectController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<Account> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherSubjectReadDto>>> GetAllTeacherSubjects()
        {
            try
            {
                var TeacherSubjects = await _unitOfWork.Repository<TeacherSubject>().GetAllWithAsync();
                var TeacherSubjectDtos = _mapper.Map<IEnumerable<TeacherSubjectReadDto>>(TeacherSubjects);

                return Ok(Result<IEnumerable<TeacherSubjectReadDto>>.Success(TeacherSubjectDtos, "Get All TeacherSubject successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<TeacherSubjectReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherSubjectReadDto>> GetTeacherSubjectById([FromQuery] int id)
        {
            try
            {
                var TeacherSubject = await _unitOfWork.Repository<TeacherSubject>().GetbyIdAsync(id);

                if (TeacherSubject == null)
                {
                    // Return a NotFound response if the TeacherSubject is not found
                    return NotFound(new ApiResponse(404, "TeacherSubject not found."));
                }

                var TeacherSubjectDto = _mapper.Map<TeacherSubjectReadDto>(TeacherSubject);
                return Ok(Result<TeacherSubjectReadDto>.Success(TeacherSubjectDto, "Get by id TeacherSubject successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<TeacherSubjectReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<ActionResult<TeacherSubjectReadDto>> CreateTeacherSubject([FromBody] TeacherSubjectCreateDto TeacherSubjectDto)
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);

                var TeacherSubject = _mapper.Map<TeacherSubject>(TeacherSubjectDto);
                TeacherSubject.Teacher_id = user.Id;
                // Fetch the Subject entities based on the IDs provided in the DTO
                // Fetch the Subject entity from the repository
                var subject = await _unitOfWork.Repository<Subject>().GetbyIdAsync(TeacherSubjectDto.SubjectId);

                // Ensure the subject exists (optional but recommended)
                if (subject == null)
                {
                    // Handle the case where the subject is not found
                    return Ok(Result<TeacherSubjectReadDto>.Fail($"Subject with ID {TeacherSubjectDto.SubjectId} not found."));
                }

                // Set the SubjectId property
                TeacherSubject.SubjectId = subject.Id;
                await _unitOfWork.Repository<TeacherSubject>().AddAsync(TeacherSubject);
                await _unitOfWork.CompleteAsync();
                var createdTeacherSubjectDto = _mapper.Map<TeacherSubjectReadDto>(TeacherSubject);

                return Ok(Result<TeacherSubjectReadDto>.Success(createdTeacherSubjectDto, "TeacherSubject created successfully"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<TeacherSubjectReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacherSubject([FromQuery] int id)
        {
            try
            {
                var TeacherSubject = await _unitOfWork.Repository<TeacherSubject>().GetbyIdAsync(id);
                if (TeacherSubject == null)
                {
                    // Return a NotFound response if the Subject is not found
                    return NotFound(new ApiResponse(404, "TeacherSubject not found."));
                }

                _unitOfWork.Repository<TeacherSubject>().DeleteAsync(TeacherSubject);
                await _unitOfWork.CompleteAsync();
                return Ok(Result<TeacherSubjectReadDto>.Success("TeacherSubject Delete successfully"));
            }
            catch (Exception ex)
            {
                return Ok(Result<TeacherSubjectReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("GetTeacherSubjectsByTeacherId")]
        public async Task<ActionResult<IEnumerable<TeacherSubjectReadDto>>> GetTeacherSubjectsByTeacherId()
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);
                var spec = new TeacherSubjectByTeacherIdSpecification(user.Id);
                var TeacherSubjects = await _unitOfWork.Repository<TeacherSubject>().GetAllWithSpecAsync(spec);

                if (TeacherSubjects == null || !TeacherSubjects.Any())
                {
                    return NotFound(Result<IEnumerable<TeacherSubjectReadDto>>.Fail("No subject found for the given TeacherId."));
                }

                var TeacherSubjectDtos = _mapper.Map<IEnumerable<TeacherSubjectReadDto>>(TeacherSubjects);
                return Ok(Result<IEnumerable<TeacherSubjectReadDto>>.Success(TeacherSubjectDtos, "Get TeacherSubjects by TeacherId successful"));
            }
            catch (Exception ex)
            {
                return Ok(Result<TeacherSubjectReadDto>.Fail(ex.Message));
            }
        }
    }
}