using Microsoft.AspNetCore.Mvc;
using Tracker.Data;
using Tracker.Dtos;
using Tracker.Helpers;
using Tracker.Models;

namespace Tracker.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("registration")]
        public IActionResult Register(RegisterDto dto)
        {
            try
            {
                var user = new User
                {
                    email = dto.email,
                    password = BCrypt.Net.BCrypt.HashPassword(dto.password),
                    name = dto.name,
                };

                if (user == null)
                {
                    return BadRequest(new { message = "Пользователь не найден" });
                }

                var candidate = _repository.GetByEmail(dto.email);

                if (candidate != null)
                {
                    return Conflict(new { message = "Пользователь с таким email уже существует" });
                }

                return Created("Success", _repository.Create(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _repository.GetByEmail(dto.email);
            if (user == null)
            {
                return BadRequest(new { message = "Пользователь не найден" });
            }
            if (!BCrypt.Net.BCrypt.Verify(dto.password, user.password))
            {
                return BadRequest(new { message = "Указан не верный пароль" });
            }

            var token = _jwtService.Generate(user.id);
            Response.Cookies.Append("token", token, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                user.email,
                token,
                user.id
            });
        }

        [HttpGet("auth")]
        public IActionResult User(LoginDto dto)
        {
            try
            {
                var jwt = Request.Cookies["token"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user = _repository.GetById(userId);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");

            return Ok(new
            {
                message = "success"
            });
        }

        [HttpDelete("remove")]
        public IActionResult Remove(RemoveDto dto)
        {
            try
            {
                Response.Cookies.Delete("token");
                _repository.RemoveByEmail(dto.email);

                return Ok(new
                {
                    message = "Пользователь удален"
                });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Неизвестная ошибка сервера" });
            }
        }
    }
}


