using LearningEnglish.BusinessLogic.Dtos.User;
using LearningEnglish.DataAccess.Entities;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> CheckLogin(UserForLoginDto userForLoginDto);

        Task<User> GetUserByUserName(string username);

        Task<string> GenerateJwtToken(User user);
    }
}
