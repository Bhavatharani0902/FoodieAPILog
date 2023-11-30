using Microsoft.AspNetCore.Mvc;
using HungryHUB.DTO;
using HungryHUB.Entity;
using HungryHUB.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using log4net;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using HungryHUB.Models;

namespace HungryHUB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IMapper mapper, ILog logger, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<User> users = _userService.GetAllUsers();
                List<UserDTO> usersDto = _mapper.Map<List<UserDTO>>(users);
                _logger.Info("Retrieved all users successfully.");
                return Ok(usersDto);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error getting all users: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult AddUser(UserDTO userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                _userService.CreateUser(user);
                _logger.Info($"User registered successfully. User ID: {user.UserID}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error registering user: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("EditUser")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult EditUser(UserDTO userDto)
        {
            try
            {
                User user = _mapper.Map<User>(userDto);
                _userService.EditUser(user);
                _logger.Info($"User updated successfully. User ID: {user.UserID}");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error updating user: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Validate")]
        [AllowAnonymous]
        public IActionResult Validate(Login login)
        {
            try
            {
                User user = _userService.ValidateUser(login.Email, login.Password);
                AuthResponse authResponse = new AuthResponse();
                if (user != null)
                {
                    authResponse.UserName = user.Name;
                    authResponse.Role = user.Role;
                    authResponse.Token = GenerateToken(user);
                }
                _logger.Info($"User validated successfully. User ID: {user?.UserID}");
                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error validating user: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        private string GenerateToken(User? user)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var identity = new ClaimsIdentity(claims);
            var expires = DateTime.UtcNow.AddMinutes(10);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
