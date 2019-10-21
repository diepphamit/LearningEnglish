using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEnglish.DataAccess.Entities
{
    public class UserCourse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}
