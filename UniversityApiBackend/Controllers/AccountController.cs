﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly JwtSettings _jwtSettings;

        public AccountController(UniversityDBContext context, JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
            _context = context;
        }

        // Example Users 
        // TODO: Change by real users in DB
        private IEnumerable<User> Logins = new List<User>()
       {
           new User()
           {
               Id = 1,
               Email="martin@gmail.com",
               Name= "Admin",
               Password="Admin"
           },
           new User(){
               Id = 2,
               Email="eros@gmail.com",
               Name= "User1",
               Password="User1"
           }
       };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();

                var valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));


                if (valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {   UserName=user.Name,
                        EmailId=user.Email,
                        Id = user.Id,
                        GuidId= Guid.NewGuid(),
                        
                    },_jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
                return Ok(Token);

            }catch (Exception ex)
            {
                throw new Exception("GetToken  Error ", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles ="Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }
}
