using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEnglish.DataAccess.Entities
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Introduce { get; set; }

        public string Image { get; set; }

        public virtual ICollection<UserCourse> UserCourses { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }
    }
}
