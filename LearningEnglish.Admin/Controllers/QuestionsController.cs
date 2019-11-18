using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Core;
using LearningEnglish.BusinessLogic.Dtos.Question;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningEnglish.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<QuestionsController> _logger;

        public QuestionsController(
            IQuestionRepository questionRepository,
            IMapper mapper
        //ILogger<CoursesController> logger
        )
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            // _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllQuestions(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                var list = _questionRepository.GetAllQuestions(keyword);

                int totalCount = list.Count();

                var query = list.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

                var response = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionForListDto>>(query);

                var paginationSet = new PaginationSet<QuestionForListDto>()
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

        [Route("GetAllQuestionName")]
        [HttpGet]
        public IActionResult GetAllQuestionName()
        {
            try
            {
                var list = _questionRepository.GetAllQuestions("");
                var listForReturn = _mapper.Map<IEnumerable<Question>, IEnumerable<QuestionNameDto>>(list);

                return Ok(listForReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError("Có lỗi trong quá trình lấy dữ liệu", ex.ToString());

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound();

            return Ok(_mapper.Map<QuestionForReturnDto>(question));
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionForCreateDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _questionRepository.CreateQuestionAsync(input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionForUpdateDto input)
        {
            if (ModelState.IsValid)
            {
                var questionInDb = await _questionRepository.GetQuestionByIdAsync(id);
                if (questionInDb == null)
                    return NotFound(id);

                var result = await _questionRepository.UpdateQuestionAsync(questionInDb, input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var questionInDb = await _questionRepository.GetQuestionByIdAsync(id);
            if (questionInDb == null)
                return NotFound(id);

            var result = await _questionRepository.DeleteQuestionAsync(questionInDb.Id);
            if (!result)
                return BadRequest("Có lỗi trong quá trình xóa dữ liệu: ");

            return Ok();
        }
    }
}