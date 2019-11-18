using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.Lesson;
using LearningEnglish.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglish.Web.Controllers
{
    [Route("lesson")]
    public class LessonController : Controller
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;
        public LessonController(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetLessons()
        {
            var lessons = await _lessonRepository.GetLessonsByCourseId(1);
            var lessonsForReturn = _mapper.Map<List<LessonForListViewModel>>(lessons);
            return View(lessonsForReturn);
        }
        [HttpGet("{CourseId}")]
        //[Route("lesson/{CourseId}")]
        public async Task<IActionResult> GetLessonsByCourseId(int CourseId)
        {
            var lessons = await _lessonRepository.GetLessonsByCourseId(CourseId);
            ViewBag.CourseId = CourseId;
            var lessonsForReturn = _mapper.Map<List<LessonForListViewModel>>(lessons);
            return View(lessonsForReturn);
        }
        [HttpGet("detail/{Id}")]
        public async Task<IActionResult> GetLessonById(int Id)
        {
            Lesson lesson = await _lessonRepository.GetLessonById(Id);
            var lessonForReturn = _mapper.Map<LessonForDetailViewModel>(lesson);
            return View(lessonForReturn);
        }
    }
}