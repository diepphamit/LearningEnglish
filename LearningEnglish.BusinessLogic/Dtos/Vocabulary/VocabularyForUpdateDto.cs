﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.Dtos.Vocabulary
{
    public class VocabularyForUpdateDto
    {
        public int id { get; set; }
        public int LessonId { get; set; }
        public string Phonetic { get; set; }
        public string Video { get; set; }
        public string Audio { get; set; }
    }
}
