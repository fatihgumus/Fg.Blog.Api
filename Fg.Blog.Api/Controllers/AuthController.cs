using System;
using Fg.Blog.Api.Loging;
using Fg.Blog.Api.Services;
using Fg.Blog.Api.ViewModels;
using Fg.Blog.API.ViewModels;
using Fg.Blog.Data.Repository;
using Fg.Blog.Model;
using Microsoft.AspNetCore.Mvc;

namespace Fg.Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        IAuthService authService;
        IUserRepository userRepository;
        ILoggerManager _logger; 

        public AuthController(IAuthService authService, IUserRepository userRepository, ILoggerManager logger)
        {
            this.authService = authService;
            this.userRepository = userRepository;
            this._logger = logger;
        }

        [HttpPost("login")]
        public ActionResult<AuthData> Post([FromBody]LoginViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = userRepository.GetSingle(u => u.Email == model.Email);

            if (user == null) {
                _logger.LogInfo(model.Email + " mail adresi giriþ yapmaya çalýþtý. Bulunamadý. "); 
                return BadRequest(new { email = "bu e-postaya sahip kullanýcý yok" });
            }

            var passwordValid = authService.VerifyPassword(model.Password, user.Password);
            if (!passwordValid) {
                _logger.LogInfo(model.Email + " mail adresi giriþ yapmaya çalýþtý. Parola Hatasý. ");
                return BadRequest(new { password = "geçersiz þifre" });
            } 
            return authService.GetAuthData(user.Id);
        }

        [HttpPost("register")]
        public ActionResult<AuthData> Post([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var emailUniq = userRepository.isEmailUniq(model.Email);
            if (!emailUniq) return BadRequest(new { email = "Bu mail adresi zaten kullanýmda" });
            var usernameUniq = userRepository.IsUsernameUniq(model.Username);
            if (!usernameUniq) return BadRequest(new { username = "Bu mail adresi zaten kullanýmda" }); 
            
            var user = new User
            { 
                Username = model.Username,
                Email = model.Email,
                Password = authService.HashPassword(model.Password)
            };
            userRepository.Add(user);
            userRepository.Commit();

            return authService.GetAuthData(user.Id);
        }

    }
}