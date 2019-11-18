using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.User;
using LearningEnglish.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace LearningEnglish.Web.Controllers
{
    [Route("[controller]/")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login()
        {
            return View(new UserForLoginViewModel());
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserForLoginViewModel userForLogin)
        {
            if (ModelState.IsValid)
            {
                if (await _authRepository.CheckLoginViewModel(userForLogin))
                {
                    var appUser = await _authRepository.GetUserByUserName(userForLogin.Username);
                    TempData["msg"] = userForLogin.Username;
                    TempData["userId"] = appUser.Id;
                }
                else
                {
                    TempData["msg"] = "0";
                    TempData["userId"] = 0;
                }
                return View(userForLogin);
            }
            return View(userForLogin);
        }
    }
}