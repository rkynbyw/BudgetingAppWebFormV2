using BudgetingAPIService.BLL.DTOs;
using BudgetingAPIService.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyRESTServices.Helpers;
using MyRESTServices.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BudgetingAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBLL _userBLL;
        private readonly AppSettings _appSettings;

        public UsersController(IUserBLL userBLL, IOptions<AppSettings> appSettings)
        {
            _userBLL = userBLL;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _userBLL.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = _userBLL.Login(userLoginDTO.Username, userLoginDTO.Password);

                var userLogin = _userBLL.GetById(user.UserID);

                // Mengambil peran dari userLogin
                var role = userLogin.Role;

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username)
                };

                // Menambahkan peran ke dalam claims jika ada
                if (!string.IsNullOrEmpty(role))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var userWithToken = new UserWithToken
                {
                    Username = user.Username,
                    Role = user.Role,
                    Token = tokenString,
                    UserID = user.UserID,
                };

                return Ok(userWithToken);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Insert(UserCreateDTO userCreateDTO)
        {
            try
            {
                _userBLL.Insert(userCreateDTO);
                return Ok("User created successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, string role)
        {
            try
            {
                _userBLL.UpdateRole(id, role);
                return Ok("User role updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _userBLL.GetById(id);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            try
            {
                var roles = _userBLL.GetRole();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}