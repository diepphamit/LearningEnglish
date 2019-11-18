using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEnglish.DataAccess.Entities
{
    public class Question
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }

        public string Content { get; set; }

        public string Name{ get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
