using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglish.Web.Controllers
{
    [Route("course")]
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        [Route("")]
        public async Task<IActionResult> GetCourses()
        {
            List<CourseForListViewModel> courses = await _courseRepository.GetCourses();
            return View(courses);
        }
    }
}