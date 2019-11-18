using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearningEnglish.Web.Models;
using Microsoft.AspNetCore.Http;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.Course;

namespace LearningEnglish.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        public HomeController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
     
        public async Task<IActionResult> Index()
        {
            List<CourseForListViewModel> courses = await _courseRepository.GetPopularCourses();
            List<CourseForListViewModel> newCourses = await _courseRepository.GetNewCourses();
            ViewBag.NewCourses = newCourses;
            return View(courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
