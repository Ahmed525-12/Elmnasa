using AutoMapper;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaApp.Specf.AppSpecf.SubscribeSubjectSpecf;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.SubjectDTOS;
using ElmnasaDomain.DTOs.SubscribeSubjectDTO;
using ElmnasaDomain.Entites.app;
using ElmnasaDomain.Entites.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace Elmnasa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeSubjectController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;

        public SubscribeSubjectController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<Account> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscribeSubjectReadDto>>> GetAllSubscribeSubjects()
        {
            try
            {
                var subscribeSubjects = await _unitOfWork.Repository<SubscribeSubject>().GetAllWithAsync();
                var subscribeSubjectDtos = _mapper.Map<IEnumerable<SubscribeSubjectReadDto>>(subscribeSubjects);

                return Ok(Result<IEnumerable<SubscribeSubjectReadDto>>.Success(subscribeSubjectDtos, "Get All SubscribeSubject successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<SubscribeSubjectReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscribeSubjectReadDto>> GetSubscribeSubjectById([FromQuery] int id)
        {
            try
            {
                var subscribeSubject = await _unitOfWork.Repository<SubscribeSubject>().GetbyIdAsync(id);

                if (subscribeSubject == null)
                {
                    // Return a NotFound response if the SubscribeSubject is not found
                    return NotFound(new ApiResponse(404, "SubscribeSubject not found."));
                }

                var subscribeSubjectDto = _mapper.Map<SubscribeSubjectReadDto>(subscribeSubject);
                return Ok(Result<SubscribeSubjectReadDto>.Success(subscribeSubjectDto, "Get by id SubscribeSubject successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<SubscribeSubjectReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Student")]
        [HttpPost]
        public async Task<ActionResult<SubscribeSubjectReadDto>> CreateSubscribeSubject([FromBody] SubscribeSubjectCreateDto subscribeSubjectDto)
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);

                var subscribeSubject = _mapper.Map<SubscribeSubject>(subscribeSubjectDto);
                subscribeSubject.Student_id = user.Id;
                // Fetch the Subject entities based on the IDs provided in the DTO

                subscribeSubject.TeacherSubject = (ICollection<TeacherSubject>)await _unitOfWork.Repository<TeacherSubject>().GetAllWithSpecAsync(new SubjectByIdsSpecification(subscribeSubjectDto.SubjectIds));

                if (subscribeSubject.TeacherSubject == null)
                {
                    // Return a NotFound response if the Subject is not found
                    return NotFound(new ApiResponse(404, "Subject not found."));
                }
                await _unitOfWork.Repository<SubscribeSubject>().AddAsync(subscribeSubject);
                await _unitOfWork.CompleteAsync();
                var createdSubscribeSubjectDto = _mapper.Map<SubscribeSubjectReadDto>(subscribeSubject);

                return Ok(Result<SubscribeSubjectReadDto>.Success(createdSubscribeSubjectDto, "SubscribeSubject created successfully"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<SubscribeSubjectReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Student")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscribeSubject([FromQuery] int id)
        {
            try
            {
                var subscribeSubject = await _unitOfWork.Repository<SubscribeSubject>().GetbyIdAsync(id);
                if (subscribeSubject == null)
                {
                    // Return a NotFound response if the Subject is not found
                    return NotFound(new ApiResponse(404, "subscribeSubject not found."));
                }

                _unitOfWork.Repository<SubscribeSubject>().DeleteAsync(subscribeSubject);
                await _unitOfWork.CompleteAsync();
                return Ok(Result<SubscribeSubjectReadDto>.Success("SubscribeSubject Delete successfully"));
            }
            catch (Exception ex)
            {
                return Ok(Result<SubscribeSubjectReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Student")]
        [HttpGet("GetSubscribeSubjectsByStudentId")]
        public async Task<ActionResult<IEnumerable<SubscribeSubjectReadDto>>> GetSubscribeSubjectsByStudentId()
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);
                var spec = new SubscribeSubjectByStudentIdSpecification(user.Id);
                var subscribeSubjects = await _unitOfWork.Repository<SubscribeSubject>().GetAllWithSpecAsync(spec);

                if (subscribeSubjects == null || !subscribeSubjects.Any())
                {
                    return NotFound(Result<IEnumerable<SubscribeSubjectReadDto>>.Fail("No subscriptions found for the given student ID."));
                }

                var subscribeSubjectDtos = _mapper.Map<IEnumerable<SubscribeSubjectReadDto>>(subscribeSubjects);
                return Ok(Result<IEnumerable<SubscribeSubjectReadDto>>.Success(subscribeSubjectDtos, "Get SubscribeSubjects by StudentId successful"));
            }
            catch (Exception ex)
            {
                return Ok(Result<SubscribeSubjectReadDto>.Fail(ex.Message));
            }
        }
    }
}