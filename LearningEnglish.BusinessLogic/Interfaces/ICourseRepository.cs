using LearningEnglish.BusinessLogic.Dtos.Course;
using LearningEnglish.BusinessLogic.ViewModels.Course;
using LearningEnglish.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface ICourseRepository
    {
        #region ViewModel
        Task<List<CourseForListViewModel>> GetCourses();
        Task<List<CourseForListViewModel>> GetNewCourses();
        Task<List<CourseForListViewModel>> GetPopularCourses();

        Task<CourseForDetailViewModel> GetCourseById(int Id);
        #endregion

        #region Dto
        IEnumerable<Course> GetCourses(string keyword);
        Task<Course> GetCourseByIdAsync(int id);

        Task<bool> CreateCouseAsync(CourseForCreateDto courseCreate);

        Task<bool> UpdateCouseAsync(Course course, CourseForUpdateDto courseUpdate);

        Task<bool> DeleteCourseAsync(int id);
        #endregion

    }
}
