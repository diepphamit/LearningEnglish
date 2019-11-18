using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglish.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglish.Web.Controllers
{
    [Route("[controller]/")]
    public class AchievementController : Controller
    {
        private readonly IAchievementRepository _achievementRepository;
        public AchievementController(IAchievementRepository achievementRepository)
        {
            _achievementRepository = achievementRepository;
        }

        [HttpGet("{id}")]
        public IActionResult GetAchievementsByUserId(int id)
        {
            var listReturn = _achievementRepository.GetAchievementsByUserIdViewModel(id);
            ViewBag.UserID = id;
            return View(listReturn);
        }
    }
}