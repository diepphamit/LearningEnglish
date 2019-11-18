using LearningEnglish.BusinessLogic.Dtos.User;
using LearningEnglish.BusinessLogic.ViewModels.User;
using LearningEnglish.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetListUsers(string keyword);
        IEnumerable<UserForListDto> GetListUsersRole(string keyword);

        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserFullByIdAsync(int id);
        Task<UserForListDto> GetUserDetailByIdAsync(int id);

        Task<(bool Succeeded, string[] Errors)> CreateUserAsync(UserForCreateDto userDto);

        Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(int id, User userInDb, UserForUpdateDto userDto);
        Task<(bool Succeeded, string[] Errors)> UpdateUserFullAsync(int id, User userInDb, UserForUpdateFullDto userDto);

        Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(User user);

        #region ViewModel
        Task<(bool Succeeded, string[] Errors)> CreateUserViewModel(UserForCreateViewModel userViewModel);

        Task<(bool Succeeded, string[] Errors)> UpdateUserViewModel(int id, User userInDb, UserForUpdateViewModel userViewModel);
        Task<User> GetUserByUsernameViewModel(string username);
        Task<User> GetUserByIdViewModel(int id);
        #endregion 
    }
}
