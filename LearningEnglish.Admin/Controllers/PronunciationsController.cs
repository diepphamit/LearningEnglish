using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Core;
using LearningEnglish.BusinessLogic.Dtos.Pronunciation;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningEnglish.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PronunciationsController : ControllerBase
    {
        private readonly IPronunciationRepository _pronunciationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PronunciationsController> _logger;

        public PronunciationsController(
            IPronunciationRepository pronunciationRepository,
            IMapper mapper
        //ILogger<CoursesController> logger
        )
        {
            _pronunciationRepository = pronunciationRepository;
            _mapper = mapper;
            // _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllPronunciations(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                var list = _pronunciationRepository.GetAllPronunciations(keyword);

                int totalCount = list.Count();

                var query = list.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

                var response = _mapper.Map<IEnumerable<Pronunciation>, IEnumerable<PronunciationForListDto>>(query);

                var paginationSet = new PaginationSet<PronunciationForListDto>()
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPronunciationById(int id)
        {
            var pronunciation = await _pronunciationRepository.GetPronunciationByIdAsync(id);
            if (pronunciation == null)
                return NotFound();

            return Ok(_mapper.Map<PronunciationForReturnDto>(pronunciation));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePronunciation([FromBody] PronunciationForCreateDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _pronunciationRepository.CreatePronunciationAsync(input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePronunciation(int id, [FromBody] PronunciationForUpdateDto input)
        {
            if (ModelState.IsValid)
            {
                var pronunciationInDb = await _pronunciationRepository.GetPronunciationByIdAsync(id);
                if (pronunciationInDb == null)
                    return NotFound(id);

                var result = await _pronunciationRepository.UpdatePronunciationAsync(pronunciationInDb, input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePronunciation(int id)
        {
            var pronunciationInDb = await _pronunciationRepository.GetPronunciationByIdAsync(id);
            if (pronunciationInDb == null)
                return NotFound(id);

            var result = await _pronunciationRepository.DeletePronunciationAsync(pronunciationInDb.Id);
            if (!result)
                return BadRequest("Có lỗi trong quá trình xóa dữ liệu: ");

            return Ok();
        }
    }
}