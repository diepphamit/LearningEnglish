using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.Dtos.Question
{
    public class QuestionForUpdateDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
    }
}
