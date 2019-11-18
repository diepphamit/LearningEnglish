using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.Dtos.Answer
{
    public class AnswerForListDto
    {
        public int Id { get; set; }
        public string QuestionName { get; set; }
        public string Content { get; set; }
        public bool CorrectAnswer { get; set; }
        public string Name { get; set; }
    }
}
