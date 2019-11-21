using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.Pronunciation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LearningEnglish.Web.Controllers
{
    [Route("pronunciation")]
    public class PronunciationController : Controller
    {
        private IPronunciationRepository _pronunciationRepository;
        private readonly IMapper _mapper;
        public PronunciationController(IPronunciationRepository pronunciationRepository, IMapper mapper)
        {
            _pronunciationRepository = pronunciationRepository;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetPronunciationsByLessonId(int LessonId)
        {
            var pronunciations = await _pronunciationRepository.GetPronunciationsByLessonId(LessonId);
            var pronunciationsForReturn = _mapper.Map<List<PronunciationForListViewModel>>(pronunciations);
            return View(pronunciationsForReturn);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPronunciationById(int Id)
        {
            var pronunciation = await _pronunciationRepository.GetPronunciationById(Id);
            var pronunciationForReturn = _mapper.Map<PronunciationForDetailViewModel>(pronunciation);
            return View(pronunciationForReturn);
        }
    }
}