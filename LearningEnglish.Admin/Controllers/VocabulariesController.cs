using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LearningEnglish.DataAccess.Entities;
using LearningEnglish.BusinessLogic.Dtos.Vocabulary;
using LearningEnglish.BusinessLogic.Core;

namespace LearningEnglish.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VocabulariesController : ControllerBase
    {
        private readonly IVocabularyRepository _vocabularyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<VocabulariesController> _logger;

        public VocabulariesController(
            IVocabularyRepository vocabularyRepository,
            IMapper mapper
        //ILogger<CoursesController> logger
        )
        {
            _vocabularyRepository = vocabularyRepository;
            _mapper = mapper;
            // _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllVocabularies(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                var list = _vocabularyRepository.GetAllVocabularies(keyword);

                int totalCount = list.Count();

                var query = list.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

                var response = _mapper.Map<IEnumerable<Vocabulary>, IEnumerable<VocabularyForListDto>>(query);

                var paginationSet = new PaginationSet<VocabularyForListDto>()
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
        public async Task<IActionResult> GetVocabularyById(int id)
        {
            var vocabulary = await _vocabularyRepository.GetVocabularyByIdAsync(id);
            if (vocabulary == null)
                return NotFound();

            return Ok(_mapper.Map<VocabularyForReturnDto>(vocabulary));
        }

        [HttpPost]
        public async Task<IActionResult> CreateVocabulary([FromBody] VocabularyForCreateDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _vocabularyRepository.CreateVocabularyAsync(input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVocabulary(int id, [FromBody] VocabularyForUpdateDto input)
        {
            if (ModelState.IsValid)
            {
                var vocabularyInDb = await _vocabularyRepository.GetVocabularyByIdAsync(id);
                if (vocabularyInDb == null)
                    return NotFound(id);

                var result = await _vocabularyRepository.UpdateVocabularyAsync(vocabularyInDb, input);
                if (result)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVocabulary(int id)
        {
            var vocabularyInDb = await _vocabularyRepository.GetVocabularyByIdAsync(id);
            if (vocabularyInDb == null)
                return NotFound(id);

            var result = await _vocabularyRepository.DeleteVocabularyAsync(vocabularyInDb.Id);
            if (!result)
                return BadRequest("Có lỗi trong quá trình xóa dữ liệu: ");

            return Ok();
        }
    }
}