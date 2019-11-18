using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Core;
using LearningEnglish.BusinessLogic.Dtos.Lesson;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningEnglish.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LessonsController> _logger;

        public LessonsController(
            ILessonRepository lessonRepository,
            IMapper mapper
        //ILogger<CoursesController> logger
        )
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
            // _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllLessons(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                var list = _lessonRepository.GetAllLessons(keyword);

                int totalCount = list.Count();

                var query = list.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

                var response = _mapper.Map<IEnumerable<Lesson>, IEnumerable<LessonForListDto>>(query);

                var paginationSet = new PaginationSet<LessonForListDto>()
                {
                    Items = response,
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
        [Route("GetAllLessonName")]
        [HttpGet]
        public IActionResult GetAllLessonName()
        {
            try
            {
                var list = _lessonRepository.GetAllLessons("");
                var listForReturn = _mapper.Map<IEnumerable<Lesson>, IEnumerable<LessonNameDto>>(list);

                return Ok(listForReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError("Có lỗi trong quá trình lấy dữ liệu", ex.ToString());

                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLessonById(int id)
        {
            var course = await _lessonRepository.GetLessonByIdAsync(id);
            if (course == null)
                return NotFound();

            return Ok(_mapper.Map<LessonForReturnDto>(course));
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromBody] LessonForCreateDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _lessonRepository.CreateLessonAsync(input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLesson(int id, [FromBody] LessonForUpdateDto input)
        {
            if (ModelState.IsValid)
            {
                var lessonInDb = await _lessonRepository.GetLessonByIdAsync(id);
                if (lessonInDb == null)
                    return NotFound(id);

                var result = await _lessonRepository.UpdateLessonAsync(lessonInDb, input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            var lessonInDb = await _lessonRepository.GetLessonByIdAsync(id);
            if (lessonInDb == null)
                return NotFound(id);

            var result = await _lessonRepository.DeleteLessonAsync(lessonInDb.Id);
            if (!result)
                return BadRequest("Có lỗi trong quá trình xóa dữ liệu: ");

            return Ok();
        }
    }
}