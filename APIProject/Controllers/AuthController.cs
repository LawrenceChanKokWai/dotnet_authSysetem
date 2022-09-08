using APIProject.Data.Control;
using APIProject.Dtos;
using APIProject.Helpers;
using APIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserContext _userContext;

        public AuthController(UserContext userContext)
        {
            _userContext = userContext;          
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginDto>> Login([FromBody] LoginDto loginDtoObj)
        {
            if(loginDtoObj == null)
            {
                return BadRequest();
            }
            var user = await _userContext.UserModels.FirstOrDefaultAsync(a => a.Email == loginDtoObj.Email);
            if(user == null)
            {
                return NotFound();
            }
            if(!EncDescPassword.VerifyHashPassword(loginDtoObj.Password, user.PasswordHash, user.PasswordSalt))
            {
                return NotFound(new
                {
                    StatusCode=404,
                    Message="Wrong Password"
                });
            }

            string token = CreateJwtToken(user);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Login Successful",
                Token = token
            });
        }

        private string CreateJwtToken(UserModel user)
        {
            var roleId = user.RoleId;
            //Payload known as claim
            List<Claim> claimList = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("RoleId", roleId.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("veryveryveryverysecret"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claimList,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
                );
            var myJwt = new JwtSecurityTokenHandler().WriteToken(token);
            return myJwt;
        }
    }
}