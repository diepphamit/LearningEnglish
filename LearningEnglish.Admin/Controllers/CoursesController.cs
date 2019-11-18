using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Core;
using LearningEnglish.BusinessLogic.Dtos.Course;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.DataAccess.Constants;
using LearningEnglish.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningEnglish.Admin.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(
            ICourseRepository courseRepository,
            IMapper mapper,
            ILogger<CoursesController> logger
        )
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllCourses(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                var list = _courseRepository.GetCourses(keyword);

                int totalCount = list.Count();

                var query = list.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                var response = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseForListDto>>(query);

                var paginationSet = new PaginationSet<CourseForListDto>()
                {
                    Items = response,
                    Total = totalCount,
                };

                return Ok(paginationSet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Có lỗi trong quá trình lấy dữ liệu", ex.ToString());

                return BadRequest();
            }
        }

        [Route("GetAllCourseName")]
        [HttpGet]
        public IActionResult GetAllCourseName()
        {
            try
            {
                var list = _courseRepository.GetCourses("");
                var listForReturn = _mapper.Map<IEnumerable<Course>, IEnumerable<CourseNameDto>>(list);

                return Ok(listForReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError("Có lỗi trong quá trình lấy dữ liệu", ex.ToString());

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseRepository.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            return Ok(_mapper.Map<CourseForReturnDto>(course));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CourseForCreateDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _courseRepository.CreateCouseAsync(input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseForUpdateDto input)
        {
            if (ModelState.IsValid)
            {
                var courseInDb = await _courseRepository.GetCourseByIdAsync(id);
                if (courseInDb == null)
                    return NotFound(id);

                var result = await _courseRepository.UpdateCouseAsync(courseInDb, input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var courseInDb = await _courseRepository.GetCourseByIdAsync(id);
            if (courseInDb == null)
                return NotFound(id);

            var result = await _courseRepository.DeleteCourseAsync(courseInDb.Id);
            if (!result)
                return BadRequest("Có lỗi trong quá trình xóa dữ liệu: ");

            return Ok();
        }
    }
}