using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Register([FromBody]UserForRegisterDto userForRegisterDto)
    {
        if (await _authRepository.UserExist(userForRegisterDto.UserName))
        {
            ModelState.AddModelError("Username","Username already Exist");
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

}    
