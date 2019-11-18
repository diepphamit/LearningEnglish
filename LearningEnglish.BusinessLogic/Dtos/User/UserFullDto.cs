using System;

namespace LearningEnglish.BusinessLogic.Dtos.User
{
    public class UserFullDto
    {
        public int Id { get; set; }


        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
