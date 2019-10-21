using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEnglish.DataAccess.Entities
{
    public class Answer
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }

        public string Content { get; set; }

        public bool CorrectAnswer { get; set; }

        public string Name { get; set; }
    }
}
