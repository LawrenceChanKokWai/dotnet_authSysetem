using APIProject.Data.Control;
using APIProject.Dtos;
using APIProject.Helpers;
using APIProject.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserContext _userContext;
        private readonly IMapper _mapper;

        public UserController(UserContext userContext, IMapper mapper)
        {
            this._userContext = userContext;
            this._mapper = mapper;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<UserModel>> GetAllEmployee()
        {
            var userList = await _userContext.UserModels.ToListAsync();
            var mappedUserList = _mapper.Map<List<UserDto>>(userList);
            return Ok(new
            {
                StatusCode=200,
                Message="Success",
                Result= mappedUserList
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserById(int id)
        {
            var user = await _userContext.UserModels.FindAsync(id);
            if(user == null)
            {
                return NotFound(new
                {
                    StatusCode=404,
                    Message="User Not Found"
                });
            }
            return Ok(new {
                StatusCode=200,
                Message="User Found",
                Result=user
            });
        }

        [HttpPost("add")]
        public async Task<ActionResult<UserDto>> AddUser([FromBody]UserDto userDtoObj)
        {
            if(userDtoObj == null)
            {
                return BadRequest(new
                {
                    StatusCode=400,
                    Message="Please send in data"
                });
            }

            var userObj = _mapper.Map<UserModel>(userDtoObj);
            userObj.UpdateDate = DateTime.Now;
            userObj.CreatedBy = 1;

            EncDescPassword.CreateHashPassword(userDtoObj.Password, out byte[] passwordHash, out byte[] passwordSalt);
            userObj.PasswordHash = passwordHash;
            userObj.PasswordSalt = passwordSalt;

            await _userContext.UserModels.AddAsync(userObj);
            await _userContext.SaveChangesAsync();
            return Ok(new
            {
                StatusCode=200,
                Message="User Added"
            });
        }

        [HttpPut("update")]
        public async Task<ActionResult<UserModel>> UpdateUser([FromBody]UserModel userObj)
        {
            var isUserExist = await _userContext.UserModels.AsNoTracking().FirstOrDefaultAsync(a => a.Id == userObj.Id);
            if(isUserExist == null)
            {
                return NotFound(new {
                    StatusCode=404,
                    Message="User Not Found"
                });
            }
            _userContext.Entry(userObj).State = EntityState.Modified;
            await _userContext.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message="User Updated"
            });
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<UserModel>> DeleteUser(int id)
        {
            var user = await _userContext.UserModels.FindAsync(id);
            if(user == null)
            {
                return NotFound(new
                {
                    StatusCode=404,
                    Message="User Not found"
                });
            }
            _userContext.UserModels.Remove(user);
            await _userContext.SaveChangesAsync();
            return Ok(new
            {
                StatusCode=200,
                Message="User Deleted"
            });
        }

    }
}
