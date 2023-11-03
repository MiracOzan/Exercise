using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/Auth")]
public class AuthController(IAuthRepository authRepository, IConfiguration configuration) : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        if (await authRepository.UserExist(userForRegisterDto.UserName))
        {
            ModelState.AddModelError("Username", "Username already Exist");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userCreate = new User
        {
            UserName = userForRegisterDto.UserName
        };

        var ToCreate = await authRepository.Register(userCreate, userForRegisterDto.Password);
        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        var user = authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var tokenhandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value);
        
        var tokenDescriptior = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, userForLoginDto.UserName)
            }),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var Token = tokenhandler.CreateToken(tokenDescriptior);
        var TokenString = tokenhandler.WriteToken(Token);

        return Ok(TokenString);
    }
}    
