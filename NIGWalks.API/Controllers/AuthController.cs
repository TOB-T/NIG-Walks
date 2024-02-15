﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using NIGWalks.API.Models.DTO;
using NIGWalks.API.Repositories;

namespace NIGWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
        }



        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser()
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
            };

            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // add roles to this user
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any()) 
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }

            return BadRequest("something went wrong");
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
               var checkPasswordResult = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    // Get Roles for this User
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token
                        var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }




                    
                }

            }

            return BadRequest("Username or Password Incorrect");


        }
    }
}
