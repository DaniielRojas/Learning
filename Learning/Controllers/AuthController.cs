using AutoMapper;
using Azure;
using Learning.Custom;
using Learning.Models;
using Learning.Models.Dtos;
using Learning.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly ILoginRepos _loginRepos;
        private readonly IMapper _mapper;
        private readonly Utilities _utilities;
        protected ResponseDto _response;


        public AuthController(ILoginRepos loginRepos, IMapper mapper, Utilities utilities)
        {
            _loginRepos = loginRepos;
            _mapper = mapper;
            _response = new ResponseDto();
            _utilities = utilities;
        }


        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new ResponseDto
                {
                    IsSuccess = false,
                    DisplayMessage = "Datos inválidos.",
                    ErrorMessage = errors
                });
            }
            try
            {
              var (success, message, user) = await _loginRepos.Login(loginDto.UsernameOrEmail, loginDto.Password);
                if (!success)
                {
                    return Unauthorized(new ResponseDto
                    {
                        IsSuccess = false,
                        DisplayMessage = message
                    });
                }

                var userResponse = _mapper.Map<UserResponseDto>(user);
                var token = _utilities.GenerateJWT(user!);

                return Ok(new ResponseDto
                {
                    IsSuccess = true,
                    DisplayMessage = message,
                    Result = new
                    {
                        User = userResponse,
                        Token = token         
                    }
                });

            }catch (ArgumentException ex)
            {
               _response.IsSuccess = false;
                _response.DisplayMessage = "Error al iniciar sesión";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error inesperado al iniciar sesión";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);

            }
        }

    }
}
