using LearningEnglish.BusinessLogic.Dtos.UserCourse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface IUserCourseRepository
    {
        #region Dto

        IEnumerable<UserCourseForListDto> GetAllUserCourses(string keyword);
        Task<UserCourseForListDto> GetUserCourseByIdAsync(int id);


        Task<bool> DeleteUserCourseAsync(int id);

        #endregion
    }
}
