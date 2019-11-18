using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.ViewModels.Achievement
{
    public class AchievementForReturnViewModel
    {
        public int Id { get; set; }

        public float Point { get; set; }

        public DateTime TestDate { get; set; }

        public string CourseName { get; set; }
    }
}
