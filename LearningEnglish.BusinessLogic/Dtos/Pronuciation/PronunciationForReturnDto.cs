using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.Dtos.Pronunciation
{
    public class PronunciationForReturnDto
    {
        public int id { get; set; }
        public int LessonId { get; set; }
        public string Phonetic { get; set; }
        public string Video { get; set; }
        public string Audio { get; set; }
        public string Name { get; set; }
    }
}
