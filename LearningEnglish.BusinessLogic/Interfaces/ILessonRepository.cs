using LearningEnglish.BusinessLogic.Dtos.Lesson;
using LearningEnglish.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface ILessonRepository
    {
        #region Dto

        IEnumerable<Lesson> GetAllLessons(string keyword);
        Task<Lesson> GetLessonByIdAsync(int id);

        Task<bool> CreateLessonAsync(LessonForCreateDto create);

        Task<bool> UpdateLessonAsync(Lesson lesson, LessonForUpdateDto lessnUpdate);

        Task<bool> DeleteLessonAsync(int id);

        IEnumerable<Course> GetAllCoursesName();

        #endregion

        #region ViewModel
        Task<List<Lesson>> GetLessonsByCourseId(int CourseId);

        Task<Lesson> GetLessonById(int Id);
        #endregion
    }
}
