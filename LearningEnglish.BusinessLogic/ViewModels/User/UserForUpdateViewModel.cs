using System;

namespace LearningEnglish.BusinessLogic.ViewModels.User
{
    public class UserForUpdateViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
