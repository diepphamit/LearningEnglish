using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEnglish.DataAccess.Entities
{
    public class Lesson
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Introduce { get; set; }

        public string Video { get; set; }

        public string Tittle { get; set; }

        public virtual ICollection<Pronunciation> Pronunciations { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Vocabulary> Vocabularies { get; set; }
    }
}
