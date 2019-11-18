using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.Dtos.Question
{
    public class QuestionForReturnDto
    {
        public int Id { get; set; }
        public string CourseId { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
    }
}
