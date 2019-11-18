using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.Dtos.Achievement
{
    public class AchievementForListDto
    {
        public int Id { get; set; }

        public float Point { get; set; }

        public DateTime TestDate { get; set; }

        public string CourseName { get; set; }
        public string UserName { get; set; }


    }
}
