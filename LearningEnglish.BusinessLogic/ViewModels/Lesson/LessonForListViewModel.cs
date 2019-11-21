using LearningEnglish.BusinessLogic.ViewModels.Pronunciation;
using LearningEnglish.BusinessLogic.ViewModels.Vocabulary;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.ViewModels.Lesson
{
    public class LessonForListViewModel
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string CourseIntroduce { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Introduce { get; set; }

        public string Title { get; set; }
        public List<PronunciationForListViewModel> Pronunciations { get; set; }

        public List<VocabularyForListViewModel> Vocabularies { get; set; }

    }
}
