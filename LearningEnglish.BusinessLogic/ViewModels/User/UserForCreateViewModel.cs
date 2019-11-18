using System.ComponentModel.DataAnnotations;

namespace LearningEnglish.BusinessLogic.ViewModels.User
{
    public class UserForCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordAgain { get; set; }
        public string PhoneNumber { get; set; }

    }
}
