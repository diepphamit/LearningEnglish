using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.ViewModels.Pronunciation
{
    public class PronunciationForDetailViewModel
    {
        // public int Id { get; set; }

        public int LessonId { get; set; }

        public string LessonName { get; set; }

        public string Name { get; set; }

        public string Phonetic { get; set; }

        public string Audio { get; set; }

        public string Video { get; set; }
    }
}
