using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglish.Web.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("register")]
        public async Task<IActionResult> Register()
        {
            return View(new UserForCreateViewModel());
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserForCreateViewModel userForCreate)
        {
            if (ModelState.IsValid)
            {
                if (userForCreate.PasswordAgain.Trim() == userForCreate.Password.Trim())
                {
                    var result = await _userRepository.CreateUserViewModel(userForCreate);
                    if (result.Succeeded)
                    {
                        var appUser = await _userRepository.GetUserByUsernameViewModel(userForCreate.UserName);
                        TempData["msg"] = userForCreate.UserName;
                        TempData["userId"] = appUser.Id;

                    }
                    else { TempData["msg"] = "0";
                        TempData["msg1"] = result.Errors[0];
                    }
                }
                else TempData["msg"] = "1";
                return View(userForCreate);
            }
            return View(userForCreate);
        }
        [HttpGet]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userRepository.GetUserByIdViewModel(id);
            var userForReturn = _mapper.Map<UserForUpdateViewModel>(user);

            return View(userForReturn);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditUser(int id, UserForUpdateViewModel userForUpdate)
        {
            if (ModelState.IsValid)
            {
                var userInDb = await _userRepository.GetUserByIdViewModel(id);
                if (userInDb == null)
                    return NotFound(id);

                var result = await _userRepository.UpdateUserViewModel(id, userInDb, userForUpdate);
                if (result.Succeeded)
                {
                    return Redirect("/home");
                }

            }

            return BadRequest(ModelState);
        }

    }
}