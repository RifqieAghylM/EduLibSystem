using eduLib.Application.Auth;
using eduLib.Core.Entities;
using eduLib.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace eduLib.API.Controllers
{
    [ApiController]
    [Route("api/Books")]
    public class BooksController : ControllerBase
    {
        private readonly AuthService _authService;

        // Mock database user untuk login
        private static readonly List<User> _mockUsers = new List<User>
        {
            new User
            {
                Username = "admin",
                Password = "Admin123",
                UserRole = Role.Admin
            },
            new User
            {
                Username = "guru",
                Password = "Guru123",
                UserRole = Role.Guru
            },
            new User
            {
                Username = "pelajar",
                Password = "Pelajar123",
                UserRole = Role.Pelajar
            }
        };

        public BooksController()
        {
            _authService = new AuthService(_mockUsers);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = _authService.Login(request.Username, request.Password);

                return Ok(new
                {
                    message = "Login berhasil",
                    username = user.Username,
                    role = user.UserRole,
                    menu = _authService.GetUserMenus(user)
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    // Request body untuk login
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}