using LearningEnglish.BusinessLogic.ViewModels.Answer;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.ViewModels.Question
{
    public class QuestionForReturnViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public List<AnswerForReturnViewModel> Answers { get; set; }
    }
}
