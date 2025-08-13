using AutoMapper;
using Learning.Models;
using Learning.Models.Dtos;
using Learning.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Learning.Controllers
{
        [Route("api/[controller]")]
        [ApiController]

        public class CourseController : ControllerBase
        {
            private readonly ICourseRepos _courseRepos;
            private readonly IMapper _mapper;
            protected ResponseDto _response;
            public CourseController(ICourseRepos courseRepos, IMapper mapper)
            {
                _courseRepos = courseRepos;
                _mapper = mapper;
                _response = new ResponseDto();
            }

             [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
             [HttpPost]
            public async Task<ActionResult<ResponseDto>> CreateCourse(CreateCourseDto courseDto)
            {
                 var userIdFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
         
            if (userIdFromToken != courseDto.InstructorId )
                {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No tienes permiso para crear cursos como otro instructor.";
                _response.ErrorMessage = new List<string> { "Acceso denegado" };
                return BadRequest(_response);
            }
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Datos de entrada inválidos.";
                    _response.ErrorMessage = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(_response);
                }
                try
                {
                    CreateCourseDto course = await _courseRepos.CreateCourse(courseDto);
                    var courseResponse = _mapper.Map<CourseResponseDto>(course);
                    _response.Result = courseResponse;
                    _response.DisplayMessage = "Curso Creado Correctamente";
                    _response.IsSuccess = true;
                    return Ok(_response);
                }
                catch (ArgumentException ex)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al crear curso ";
                    _response.ErrorMessage = new List<string> { ex.Message };
                    return BadRequest(_response);
                }
                catch (Exception ex)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al crear curso ";
                    _response.ErrorMessage = new List<string> { ex.Message };
                    return StatusCode(500, _response);
                }
            }
        }
  }

