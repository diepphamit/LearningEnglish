using System.ComponentModel.DataAnnotations;

namespace LearningEnglish.BusinessLogic.Dtos.User
{
    public class UserForLoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
