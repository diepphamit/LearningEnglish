using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEnglish.DataAccess.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public int LessonId { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }

        public string Content { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
