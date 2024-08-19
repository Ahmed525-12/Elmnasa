using AutoMapper;
using ElmnasaApp.ErrorHandler;
using ElmnasaApp.Genrics.Intrefaces;
using ElmnasaApp.Wrapper.WorkWrapper;
using ElmnasaDomain.DTOs.GradesDTO;
using ElmnasaDomain.DTOs.StudentDTOs;
using ElmnasaDomain.Entites.app;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Elmnasa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student,Admin")]
    public class GradesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GradesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

                // Map the DTO to the Grades entity
                var mapped = _mapper.Map<GradeDto, Grades>(model);

                // Check if the mapping failed
                if (mapped == null)
                {
                    // Return a BadRequest response with an error message
                    return BadRequest(new ApiResponse(400, "Failed to map GradeDTO to Grades."));
                }

                // Add the Grades to the repository asynchronously
                await _unitOfWork.Repository<Grades>().AddAsync(mapped);
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
    }
}