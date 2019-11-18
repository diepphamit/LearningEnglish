 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Core;
using LearningEnglish.BusinessLogic.Dtos.Achievement;
using LearningEnglish.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningEnglish.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly IAchievementRepository _achievementRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AchievementsController> _logger;

        public AchievementsController(
            IAchievementRepository achievementRepository,
            IMapper mapper
        //ILogger<CoursesController> logger
        )
        {
            _achievementRepository = achievementRepository;
            _mapper = mapper;
            // _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllAchievements(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                var list = _achievementRepository.GetAllAchievements(keyword);

                int totalCount = list.Count();

                var query = list.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

                //var response = _mapper.Map<IEnumerable<Lesson>, IEnumerable<LessonForListDto>>(query);

                var paginationSet = new PaginationSet<AchievementForListDto>()
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
        public async Task<IActionResult> GetAchievementById(int id)
        {
            var achinDb = await _achievementRepository.GetAchievementByIdAsync(id);
            if (achinDb == null)
                return NotFound();

            return Ok(achinDb);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAchievement(int id)
        {
            var achinDb = await _achievementRepository.GetAchievementByIdAsync(id);
            if (achinDb == null)
                return NotFound(id);

            var result = await _achievementRepository.DeleteAchievementAsync(achinDb.Id);
            if (!result)
                return BadRequest("Có lỗi trong quá trình xóa dữ liệu: ");

            return Ok();
        }
    }
}