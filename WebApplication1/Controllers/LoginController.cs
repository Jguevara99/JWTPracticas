using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtPractica.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;


        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hola { currentUser.FirstName }, tu eres {currentUser.Rol}");
        }

        [HttpPost]
        public IActionResult Login(LoginUser userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);

               return Ok("usuario logueado");
            }

            return NotFound("usuario no encontrado");
        }

        private UserModel Authenticate( LoginUser userLogin)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(user => user.Username.ToLower() == userLogin.UserName.ToLower() && user.Password == userLogin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }

        private string Generate(UserModel user)
        {
            var seguritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:key"]));
            var credentials = new SigningCredentials(seguritykey, SecurityAlgorithms.HmacSha256);

            //crear los clains

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Rol)
            };


            //crear token

            var token = new JwtSecurityToken(
                 _config["Jwt:Issuer"],
                 _config["Jwt:Audience"],
                 claims,
                 expires: DateTime.Now.AddMinutes(60),
                 signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userclaims = identity.Claims;

                return new UserModel
                {
                    Username = userclaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    FirstName = userclaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    LastName = userclaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    EmailAddress = userclaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Rol = userclaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,

                };
            }

            return null;
        }

    }
}
