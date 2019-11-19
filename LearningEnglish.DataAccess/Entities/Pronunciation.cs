using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEnglish.DataAccess.Entities
{
    public class Pronunciation
    {
        public int Id { get; set; }

        public int LessonId { get; set; }

        [ForeignKey("LessonId")]
        public virtual Lesson Lesson { get; set; }

        public string Phonetic { get; set; }

        public string Video { get; set; }

        public string Audio { get; set; }
        public string Name { get; set; }
    }
}
