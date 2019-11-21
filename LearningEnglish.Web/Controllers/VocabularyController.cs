using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.Vocabulary;
using Microsoft.AspNetCore.Mvc;

namespace LearningEnglish.Web.Controllers
{
    [Route("vocabulary")]
    public class VocabularyController : Controller
    {
        private IVocabularyRepository _vocabularyRepository;
        private readonly IMapper _mapper;
        public VocabularyController(IVocabularyRepository vocabularyRepository, IMapper mapper)
        {
            _vocabularyRepository = vocabularyRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetVocabulariesByLessonId(int LessonId)
        {
            var vocabularies = await _vocabularyRepository.GetVocabulariesByLessonId(LessonId);
            var vocabulariesForReturn = _mapper.Map<List<VocabularyForListViewModel>>(vocabularies);
            return View(vocabulariesForReturn);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetVocabularyById(int Id)
        {
            var vocabulary = await _vocabularyRepository.GetVocabularyById(Id);
            var vocabularyForReturn = _mapper.Map<VocabularyForDetailViewModel>(vocabulary);
            return View(vocabularyForReturn);
        }
    }
}