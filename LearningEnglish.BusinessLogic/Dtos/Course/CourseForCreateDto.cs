﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEnglish.BusinessLogic.Dtos.Course
{
    public class CourseForCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Introduce { get; set; }
        public string Image { get; set; }
        public int LevelClass { get; set; }
    }
}
