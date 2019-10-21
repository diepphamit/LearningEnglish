using LearningEnglish.BusinessLogic.Dtos.User;
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

        Task<User> GetUserByIdAsync(int id);

        Task<(bool Succeeded, string[] Errors)> CreateUserAsync(UserForCreateDto userDto);

        Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(int id, User userInDb, UserForUpdateDto userDto);

        Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(User user);
    }
}
