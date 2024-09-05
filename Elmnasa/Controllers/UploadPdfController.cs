using AutoMapper;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaApp.Specf.AppSpecf.SubscribeSubjectSpecf;
using ElmnasaApp.Specf.AppSpecf.UploadPdfSpecf;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.UploadPdfDTOs;
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
    public class UploadPdfController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Account> _userManager;

        public UploadPdfController(IMapper mapper, IUnitOfWork unitOfWork, UserManager<Account> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UploadPdfReadDto>>> GetAllUploadPdfs()
        {
            try
            {
                var UploadPdfs = await _unitOfWork.Repository<UploadPdf>().GetAllWithAsync();
                var UploadPdfDtos = _mapper.Map<IEnumerable<UploadPdfReadDto>>(UploadPdfs);

                return Ok(Result<IEnumerable<UploadPdfReadDto>>.Success(UploadPdfDtos, "Get All UploadPdf successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<UploadPdfReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UploadPdfReadDto>> GetUploadPdfById([FromQuery] int id)
        {
            try
            {
                var UploadPdf = await _unitOfWork.Repository<UploadPdf>().GetbyIdAsync(id);

                if (UploadPdf == null)
                {
                    // Return a NotFound response if the UploadPdf is not found
                    return NotFound(new ApiResponse(404, "UploadPdf not found."));
                }

                var UploadPdfDto = _mapper.Map<UploadPdfReadDto>(UploadPdf);
                return Ok(Result<UploadPdfReadDto>.Success(UploadPdfDto, "Get by id UploadPdf successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<UploadPdfReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpPost]
        public async Task<ActionResult<UploadPdfReadDto>> CreateUploadPdf([FromBody] UploadPdfCreateDto UploadPdfDto)
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);

                var UploadPdf = _mapper.Map<UploadPdf>(UploadPdfDto);
                UploadPdf.Teacher_id = user.Id;
                // Fetch the Subject entities based on the IDs provided in the DTO

                UploadPdf.Subject = (ICollection<Subject>)await _unitOfWork.Repository<Subject>().GetAllWithSpecAsync(new SubjectByIdsSpecification(UploadPdfDto.SubjectIds));

                if (UploadPdf.Subject == null)
                {
                    // Return a NotFound response if the Subject is not found
                    return NotFound(new ApiResponse(404, "Subject not found."));
                }
                await _unitOfWork.Repository<UploadPdf>().AddAsync(UploadPdf);
                await _unitOfWork.CompleteAsync();
                var createdUploadPdfDto = _mapper.Map<UploadPdfReadDto>(UploadPdf);

                return Ok(Result<UploadPdfReadDto>.Success(createdUploadPdfDto, "UploadPdf created successfully"));
            }
            catch (Exception ex)
            {
                // Log the exception details here if logging is set up

                // Return a failure response with the exception message
                return Ok(Result<UploadPdfReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUploadPdf([FromQuery] int id)
        {
            try
            {
                var UploadPdf = await _unitOfWork.Repository<UploadPdf>().GetbyIdAsync(id);
                if (UploadPdf == null)
                {
                    // Return a NotFound response if the Subject is not found
                    return NotFound(new ApiResponse(404, "UploadPdf not found."));
                }

                _unitOfWork.Repository<UploadPdf>().DeleteAsync(UploadPdf);
                await _unitOfWork.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Ok(Result<UploadPdfReadDto>.Fail(ex.Message));
            }
        }

        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("GetUploadPdfsByTeacherId")]
        public async Task<ActionResult<IEnumerable<UploadPdfReadDto>>> GetUploadPdfsByTeacherId()
        {
            try
            {
                // Retrieve the user's email from the claims
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                // Fetch the user details based on the retrieved email
                var user = await _userManager.FindByEmailAsync(userEmail);
                var spec = new UploadPdfByTeacherIdSpecification(user.Id);
                var UploadPdfs = await _unitOfWork.Repository<UploadPdf>().GetAllWithSpecAsync(spec);

                if (UploadPdfs == null || !UploadPdfs.Any())
                {
                    return NotFound(Result<IEnumerable<UploadPdfReadDto>>.Fail("No subscriptions found for the given Teacher ID."));
                }

                var UploadPdfDtos = _mapper.Map<IEnumerable<UploadPdfReadDto>>(UploadPdfs);
                return Ok(Result<IEnumerable<UploadPdfReadDto>>.Success(UploadPdfDtos, "Get UploadPdfs by TeacherId successful"));
            }
            catch (Exception ex)
            {
                return Ok(Result<UploadPdfReadDto>.Fail(ex.Message));
            }
        }
    }
}