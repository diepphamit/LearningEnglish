using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglish.Web.Controllers
{
    [Route("[controller]")]
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
            //ViewBag.test = new CourseForListViewModel { Id = 12, Image = "sadad", Introduce = "adD", Name = "DdD" };
            //ViewBag.test = 1;
            //ViewBag.test = "sdadad";
           // var x = HttpContext.Session.GetString("Username");

            return View(courses);
        }
    }
}