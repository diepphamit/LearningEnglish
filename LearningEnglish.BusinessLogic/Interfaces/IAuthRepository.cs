using LearningEnglish.BusinessLogic.Dtos.User;
using LearningEnglish.BusinessLogic.ViewModels.User;
using LearningEnglish.DataAccess.Entities;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> CheckLogin(UserForLoginDto userForLoginDto);

        Task<User> GetUserByUserName(string username);

        Task<string> GenerateJwtToken(User user);

        #region ViewModel
        Task<bool> CheckLoginViewModel(UserForLoginViewModel userForLoginViewModel);
        #endregion
    }
}
