namespace LearningEnglish.BusinessLogic.Dtos.User
{
    public class UserForUpdateDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string[] Roles { get; set; }
    }
}
