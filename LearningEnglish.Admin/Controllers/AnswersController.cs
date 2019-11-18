using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Core;
using LearningEnglish.BusinessLogic.Dtos.Answer;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningEnglish.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AnswersController> _logger;

        public AnswersController(
            IAnswerRepository answerRepository,
            IMapper mapper
        //ILogger<CoursesController> logger
        )
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
            // _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllAnswers(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                var list = _answerRepository.GetAllAnswers(keyword);

                int totalCount = list.Count();

                var query = list.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

                var response = _mapper.Map<IEnumerable<Answer>, IEnumerable<AnswerForListDto>>(query);

                var paginationSet = new PaginationSet<AnswerForListDto>()
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
        public async Task<IActionResult> GetAnswerById(int id)
        {
            var answer = await _answerRepository.GetAnswerByIdAsync(id);
            if (answer == null)
                return NotFound();

            return Ok(_mapper.Map<AnswerForReturnDto>(answer));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] AnswerForCreateDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _answerRepository.CreateAnswerAsync(input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswer(int id, [FromBody] AnswerForUpdateDto input)
        {
            if (ModelState.IsValid)
            {
                var answerInDb = await _answerRepository.GetAnswerByIdAsync(id);
                if (answerInDb == null)
                    return NotFound(id);

                var result = await _answerRepository.UpdateAnswerAsync(answerInDb, input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnser(int id)
        {
            var answerInDb = await _answerRepository.GetAnswerByIdAsync(id);
            if (answerInDb == null)
                return NotFound(id);

            var result = await _answerRepository.DeleteAnswerAsync(answerInDb.Id);
            if (!result)
                return BadRequest("Có lỗi trong quá trình xóa dữ liệu: ");

            return Ok();
        }
    }
}