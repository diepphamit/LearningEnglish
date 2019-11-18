using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels;
using LearningEnglish.BusinessLogic.ViewModels.Question;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglish.Web.Controllers
{
    [Route("[controller]/")]
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        [HttpGet("test/{id}")]
        public async Task<IActionResult> Test(int id)
        {
            List<QuestionForReturnViewModel> list = await _questionRepository.GetQuestionByIdCourseViewModel(id);
            ViewBag.ID = id;
            return View(list);
        }

        [HttpPost("test/{id}")]
        //[Route("register")]
        public async Task<IActionResult> TestResult(List<int> questionId, List<int> listAnswer, string courseId, string userId)
        {
            if (ModelState.IsValid)
            {
                var s = await _questionRepository.GetPoint(questionId, listAnswer, courseId, userId);
                return Content("<h1>So diem cua ban la" + s +"</h1>");
            }
            return Content("<h1>Loi</h1>");
        }
    }
}