using CleanArchMvc.API.Models;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        public TokenController(IAuthenticate authenticate, IConfiguration configuration, ITokenService tokenService)
        {
            _authenticate = authenticate;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(LoginModel loginModel)
        {
            var user = await _authenticate.RegisterUserAsync(loginModel.Email, loginModel.Password);
            if (!user)
            {
                ModelState.AddModelError("Erros", "Invalid user Create Attempt.");
                return BadRequest(ModelState);
            }
            return Ok($"User {loginModel.Email} was create successfully ");
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserTokenDTO>> Login(LoginModel userInfo)
        {
            var result = await _authenticate.AuthenticateAsync(userInfo.Email, userInfo.Password);
            if (!result)
            {
                ModelState.AddModelError("Erros", "Invalid Login Attempt.");
                return BadRequest(ModelState);
            }
            return GenerateToken(userInfo);
        }

        private UserTokenDTO GenerateToken(LoginModel userInfo)
        {

            var token = _tokenService.GenerateToken(userInfo.Email);

            return new UserTokenDTO()
            {
                Token = token

            };

        }
    }
}
