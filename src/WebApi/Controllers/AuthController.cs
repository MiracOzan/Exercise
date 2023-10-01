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
public class AuthController : Controller
{
    private IAuthRepository _authRepository;
    private IConfiguration _configuration;

    public AuthController(IAuthRepository authRepository, IConfiguration configuration)
    {
        _authRepository = authRepository;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
    {
        if (await _authRepository.UserExist(userForRegisterDto.UserName))
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

        var ToCreate = await _authRepository.Register(userCreate, userForRegisterDto.Password);
        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
    {
        var user = _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);

        if (user == null)
        {
            return Unauthorized();
        }

        var tokenhandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
        
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
