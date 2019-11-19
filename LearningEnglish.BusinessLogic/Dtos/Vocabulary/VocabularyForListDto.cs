using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.Dtos.Vocabulary
{
    public class VocabularyForListDto
    {
        public int id { get; set; }
        public string LessonName { get; set; }
        public string Phonetic { get; set; }
        public string Video { get; set; }
        public string Audio { get; set; }
        public string Name { get; set; }
    }
}
