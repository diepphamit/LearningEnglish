using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.Dtos.Lesson
{
    public class LessonForCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
        public string Type { get; set; }
        public string Introduce { get; set; }
        public string Video { get; set; }
        public string Tittle { get; set; }
    }
}
