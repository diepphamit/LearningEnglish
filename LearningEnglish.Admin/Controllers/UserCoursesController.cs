using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Core;
using LearningEnglish.BusinessLogic.Dtos.UserCourse;
using LearningEnglish.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningEnglish.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCoursesController : ControllerBase
    {
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserCoursesController> _logger;

        public UserCoursesController(
            IUserCourseRepository userCourseRepository,
            IMapper mapper
        //ILogger<CoursesController> logger
        )
        {
            _userCourseRepository = userCourseRepository;
            _mapper = mapper;
            // _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllUserCourses(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                var list = _userCourseRepository.GetAllUserCourses(keyword);

                int totalCount = list.Count();

                var query = list.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

                //var response = _mapper.Map<IEnumerable<Lesson>, IEnumerable<LessonForListDto>>(query);

                var paginationSet = new PaginationSet<UserCourseForListDto>()
                {
                    Items = query,
                    Total = totalCount,
                };

                return Ok(paginationSet);
            }
            catch (Exception ex)
            {
                //_logger.LogError("Có lỗi trong quá trình lấy dữ liệu", ex.ToString());

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserCourseById(int id)
        {
            var ucDb = await _userCourseRepository.GetUserCourseByIdAsync(id);
            if (ucDb == null)
                return NotFound();

            return Ok(ucDb);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCourse(int id)
        {
            var ucDb = await _userCourseRepository.GetUserCourseByIdAsync(id);
            if (ucDb == null)
                return NotFound(id);

            var result = await _userCourseRepository.DeleteUserCourseAsync(ucDb.Id);
            if (!result)
                return BadRequest("Có lỗi trong quá trình xóa dữ liệu: ");

            return Ok();
        }
    }
}