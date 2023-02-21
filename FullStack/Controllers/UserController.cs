using FullStack.Data;
using FullStack.Hashing;
using FullStack.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FullStack.Controllers
{
    //Routing
    [Route("api/[controller]")]
    //This attribute tells .NET framework this is an API controller
    [ApiController]
    public class UserController : Controller
    {
        private readonly FullStackDbContext fullStackDbContext;

        public UserController(FullStackDbContext fullStackDbContext)
        {
            this.fullStackDbContext = fullStackDbContext;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = fullStackDbContext.Users.FirstOrDefault
                (u => u.UserName == userObj.UserName );

            if (user == null)
                return NotFound(new { Message = "User Not Found!!!" });

            if (!PasswordHash.VerifyPassword(userObj.Passwords, user.Passwords))
            {
                return BadRequest(new { Message = "Password is incorrect" });
            }

            user.Token = CreateJwt(user);

            return Ok(new
            {
                Token = user.Token,
                Message = "Login Success!!!"
            });
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            //Check email
            if (CheckEmailExist(userObj.Email))
                return BadRequest(new { Message = "Email already exsist" });

            //Check username
            if (CheckUserNameExist(userObj.UserName))
                return BadRequest(new { Message = "Username already exsist" });

            //Check password strength
            var pass = CheckPasswordStrength(userObj.Passwords);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() });

            //Hasing the password
            userObj.Passwords = PasswordHash.HashPassword(userObj.Passwords);
            userObj.Roles = "Employee";
            userObj.Token = "";
            fullStackDbContext.Users.Add(userObj);
            fullStackDbContext.SaveChanges();
            return Ok(new
            {
                Message = "User Registered!"
            });
        }

        private bool CheckUserNameExist(string username)
        {
            return fullStackDbContext.Users.Any(x => x.UserName == username);
        }

        private bool CheckEmailExist(string email)
        {
            return fullStackDbContext.Users.Any(x => x.Email == email);
        }

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Minimum password length is 8" + Environment.NewLine);

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]")
              && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be Alphanumeric" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
            {
                sb.Append("Password should contain special characters" + Environment.NewLine);
            }
            return sb.ToString();

        }

        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret...");
            var identity = new ClaimsIdentity(new Claim[]
            {
      new Claim(ClaimTypes.Role, user.Roles),
      new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(fullStackDbContext.Users.ToList());
        }
    }
}
