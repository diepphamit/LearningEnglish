using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEnglish.DataAccess.Entities
{
    public class Achievement
    {
        public int Id { get; set; }

        public float Point { get; set; }

        public DateTime TestDate { get; set; }

        public int LessonId { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }
    }
}
