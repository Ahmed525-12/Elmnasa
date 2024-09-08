using AutoMapper;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaApp.Specf.AppSpecf.SubscribeSubjectSpecf;
using ElmnasaApp.Specf.AppSpecf.UploadVideoSpecf;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.UploadVideosDTOS;
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
    public class UploadVideoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;

        public UploadVideoController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<Account> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UploadVideoReadDto>>> GetAllUploadVideos()
        {
            try
            {
                var UploadVideos = await _unitOfWork.Repository<UploadVideo>().GetAllWithAsync();
                var UploadVideoDtos = _mapper.Map<IEnumerable<UploadVideoReadDto>>(UploadVideos);

                return Ok(Result<IEnumerable<UploadVideoReadDto>>.Success(UploadVideoDtos, "Get All UploadVideo successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<UploadVideoReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UploadVideoReadDto>> GetUploadVideoById([FromQuery] int id)
        {
            try
            {
                var specf = new UploadVideoGetbyIdSpecf(id);
                var UploadVideo = await _unitOfWork.Repository<UploadVideo>().GetAllWithSpecAsync(specf);

                if (UploadVideo == null)
                {
                    // Return a NotFound response if the UploadVideo is not found
                    return NotFound(new ApiResponse(404, "UploadVideo not found."));
                }

                var UploadVideoDto = _mapper.Map<List<UploadVideoReadDto>>(UploadVideo);
                return Ok(Result<List<UploadVideoReadDto>>.Success(UploadVideoDto, "Get by id UploadVideo successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<UploadVideoReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<ActionResult<UploadVideoReadDto>> CreateUploadVideo([FromBody] UploadVideoCreateDto UploadVideoDto)
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);

                var UploadVideo = _mapper.Map<UploadVideo>(UploadVideoDto);
                UploadVideo.Teacher_id = user.Id;
                // Fetch the Subject entities based on the IDs provided in the DTO

                UploadVideo.Subject = (ICollection<Subject>)await _unitOfWork.Repository<Subject>().GetAllWithSpecAsync(new SubjectByIdsSpecification(UploadVideoDto.SubjectIds));

                if (UploadVideo.Subject == null)
                {
                    // Return a NotFound response if the Subject is not found
                    return NotFound(new ApiResponse(404, "Subject not found."));
                }
                await _unitOfWork.Repository<UploadVideo>().AddAsync(UploadVideo);
                await _unitOfWork.CompleteAsync();
                var createdUploadVideoDto = _mapper.Map<UploadVideoReadDto>(UploadVideo);

                return Ok(Result<UploadVideoReadDto>.Success(createdUploadVideoDto, "UploadVideo created successfully"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<UploadVideoReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUploadVideo([FromQuery] int id)
        {
            try
            {
                var UploadVideo = await _unitOfWork.Repository<UploadVideo>().GetbyIdAsync(id);
                if (UploadVideo == null)
                {
                    // Return a NotFound response if the Subject is not found
                    return NotFound(new ApiResponse(404, "UploadVideo not found."));
                }

                _unitOfWork.Repository<UploadVideo>().DeleteAsync(UploadVideo);
                await _unitOfWork.CompleteAsync();
                return Ok(Result<UploadVideoReadDto>.Success("UploadVideo delete successfully"));
            }
            catch (Exception ex)
            {
                return Ok(Result<UploadVideoReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("GetUploadVideosByTeacherId")]
        public async Task<ActionResult<IEnumerable<UploadVideoReadDto>>> GetUploadVideosByTeacherId()
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);
                var spec = new UploadVideoByTeacherIdSpecification(user.Id);
                var UploadVideos = await _unitOfWork.Repository<UploadVideo>().GetAllWithSpecAsync(spec);

                if (UploadVideos == null || !UploadVideos.Any())
                {
                    return NotFound(Result<IEnumerable<UploadVideoReadDto>>.Fail("No subscriptions found for the given Teacher ID."));
                }

                var UploadVideoDtos = _mapper.Map<IEnumerable<UploadVideoReadDto>>(UploadVideos);
                return Ok(Result<IEnumerable<UploadVideoReadDto>>.Success(UploadVideoDtos, "Get UploadVideos by TeacherId successful"));
            }
            catch (Exception ex)
            {
                return Ok(Result<UploadVideoReadDto>.Fail(ex.Message));
            }
        }
    }
}