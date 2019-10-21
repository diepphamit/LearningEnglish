using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEnglish.DataAccess.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public string Content { get; set; }
    }
}
