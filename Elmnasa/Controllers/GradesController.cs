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
    [Authorize(Roles = "Student,Admin")]
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

        [HttpPost("CreateGrade")]
        public async Task<ActionResult<GradeDto>> CreateGrade(GradeDto model)
        {
            try
            {
                // Check if the model is null
                if (model == null)
                {
                    // Return a BadRequest response with an error message
                    return BadRequest(new ApiResponse(400, "Invalid request data."));
                }
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(userEmail);

                var grade = new Grades()
                {
                    Name = model.Name,
                    Student_id = user.Id
                };

                // Add the Grades to the repository asynchronously
                await _unitOfWork.Repository<Grades>().AddAsync(grade);
                await _unitOfWork.CompleteAsync();

                return Ok(Result<GradeDto>.Success(model, "Create successful"));
            }
            catch (Exception ex)
            {
                // Log the exception details

                // Return a more detailed error response
                return Ok(Result<GradeDto>.Fail(ex.Message));
            }
        }

        [HttpPost("UpdateGrade")]
        public async Task<ActionResult<GradeDto>> UpdateGrade(UpdateGradeDTO model)
        {
            try
            {
                // Check if the model is null
                if (model == null)
                {
                    // Return a BadRequest response with an error message
                    return BadRequest(new ApiResponse(400, "Invalid request data."));
                }
                // Map the DTO to the Grades entity
                var mapped = _mapper.Map<Grades>(model);

                // Check if the mapping failed
                if (mapped == null)
                {
                    // Return a BadRequest response with an error message
                    return BadRequest(new ApiResponse(400, "Failed to map GradeDto to Grades."));
                }

                // update the Grades to the repository asynchronously
                _unitOfWork.Repository<Grades>().Update(mapped);
                await _unitOfWork.CompleteAsync();

                return Ok(Result<Grades>.Success(mapped, "update successful"));
            }
            catch (Exception ex)
            {
                // Return a more detailed error response
                return Ok(Result<GradeDto>.Fail(ex.Message));
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<GradeDto>> GetGradeById(int id)
        {
            try
            {
                var grade = await _unitOfWork.Repository<Grades>().GetbyIdAsync(id);

                if (grade is null)
                    return NotFound(new ApiResponse(404));
                var MappedItem = _mapper.Map<GradeDto>(grade);
                return Ok(Result<GradeDto>.Success(MappedItem, "get successful"));
            }
            catch (Exception ex)
            {
                // Return a more detailed error response
                return Ok(Result<GradeDto>.Fail(ex.Message));
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            try
            {
                var grade = await _unitOfWork.Repository<Grades>().GetbyIdAsync(id);

                if (grade == null)
                {
                    return NotFound(new ApiResponse(404, "grade not found"));
                }

                _unitOfWork.Repository<Grades>().DeleteAsync(grade);
                await _unitOfWork.CompleteAsync();  // Assuming that Complete() is the method that saves changes

                return Ok(Result<GradeDto>.Success("delete successful"));
            }
            catch (Exception ex)
            {
                // Return a more detailed error response
                return Ok(Result<GradeDto>.Fail(ex.Message));
            }
        }

        [HttpGet]
        public async Task<ActionResult<GradeDto>> GetAllGrades()
        {
            try
            {
                var grades = await _unitOfWork.Repository<Grades>().GetAllWithAsync();

                return Ok(grades);
            }
            catch (Exception ex)
            {
                return Ok(Result<GradeDto>.Fail(ex.Message));
            }
        }

        [HttpGet("GetAllUserGrades")]
        public async Task<ActionResult<GradeDto>> GetAllUserGrades()
        {
            try
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                var user = await _userManager.FindByEmailAsync(userEmail);
                var spec = new UserGradeSpecf(user.Id);
                var monthOfEnter = await _unitOfWork.Repository<Grades>().GetAllWithSpecAsync(spec);
                var MappedResult = _mapper.Map<List<Grades>>(monthOfEnter);
                return Ok(MappedResult);
            }
            catch (Exception ex)
            {
                return Ok(Result<GradeDto>.Fail(ex.Message));
            }
        }
    }
}