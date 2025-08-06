using AutoMapper;
using Learning.Models;
using Learning.Models.Dtos;
using Learning.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Learning.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepos _userRepos;
        private readonly IMapper _mapper;
        protected ResponseDto _response;

        public UserController(IUserRepos userRepos , IMapper mapper)
        {
            _userRepos = userRepos;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateUser(CreateUserDto userDto)
        {
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
                CreateUserDto user = await _userRepos.CreateUser(userDto);
                var userResponse = _mapper.Map<UserResponseDto>(user);
                _response.Result = userResponse;
                _response.DisplayMessage = "Usuario Creado Correctamente";
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (ArgumentException ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear usuario ";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error inespeado al crear usuario";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }
    }
}
