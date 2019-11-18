using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglish.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglish.Web.Controllers
{
    [Route("[controller]/")]
    public class AnswerController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        public AnswerController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        [HttpGet("test/{id}")]
        public async Task<IActionResult> Test(int id)
        {
            var list = await _questionRepository.GetQuestionByIdCourseViewModel(id);
            return View(list);
        }
    }
}