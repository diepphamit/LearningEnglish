using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningEnglish.DataAccess.Entities
{
    public class Achievement
    {
        public int Id { get; set; }

        public float Point { get; set; }

        public DateTime TestDate { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        public int UserId { get; set; }
    }
}
