using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.Dtos.Answer
{
    public class AnswerForCreateDto
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Content { get; set; }
        public bool CorrectAnswer { get; set; }
        public string Name { get; set; }
    }
}
