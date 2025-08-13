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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepos _userRepos;
        private readonly IMapper _mapper;
        protected ResponseDto _response;

        public UserController(IUserRepos userRepos, IMapper mapper)
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

        [HttpGet("all")]
        public async Task<ActionResult<ResponseDto>> SeeUsers()
        {
            try
            {
                var users = await _userRepos.SeeUsers();
                _response.Result = users;
                _response.DisplayMessage = "Lista de usuarios obtenida correctamente.";
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = new List<string> { ex.InnerException?.Message ?? ex.Message };
                _response.DisplayMessage = "No se encontraron usuarios.";
                return NotFound(_response);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error inesperado al obtener la lista de usuarios.";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetUserById(int id)
        {
            try
            {
                var user = await _userRepos.GetUserById(id);
                if (user == null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Usuario no encontrado.";
                    return NotFound(_response);
                }

                _response.Result = user;
                _response.DisplayMessage = "Usuario obtenido correctamente.";
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario no encontrado.";
                _response.ErrorMessage = new List<string> { ex.Message };
                return NotFound(_response);
            }
            catch (ArgumentException ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener el usuario.";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al procesar la solicitud.";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error inesperado al obtener el usuario.";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("update/{id}")]

        public async Task<ActionResult<ResponseDto>> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var userIdFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userIdFromToken != id)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No tienes permiso para actualizar este usuario.";
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
                var user = await _userRepos.UpdateUser(id,  updateUserDto);
                var userResponse = _mapper.Map<UserResponseDto>(user);
                _response.Result = userResponse;
                _response.DisplayMessage = "Usuario actualizado correctamente.";
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (ArgumentException ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el usuario.";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error inesperado al actualizar el usuario.";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteUser(int id)
        {
            var userIdFromToken = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userIdFromToken != id)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "No tienes permiso para eliminar este usuario.";
                _response.ErrorMessage = new List<string> { "Acceso denegado" };
                return BadRequest(_response);
            }
            try
            {
                bool user = await _userRepos.DeleteUser(id);
                if (!user)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Usuario no encontrado.";
                    _response.Result = null;
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.DisplayMessage = "Usuario eliminado correctamente.";
                _response.Result = null;  
                return Ok(_response);
            }
            catch (ArgumentException ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al eliminar el usuario.";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error inesperado al eliminar el usuario.";
                _response.ErrorMessage = new List<string> { ex.Message };
                return BadRequest(_response);
            }
        }

    }
}
