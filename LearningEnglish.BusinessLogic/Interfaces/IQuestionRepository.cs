using LearningEnglish.BusinessLogic.Dtos.Question;
using LearningEnglish.BusinessLogic.ViewModels.Question;
using LearningEnglish.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface IQuestionRepository
    {
        #region Dto

        IEnumerable<Question> GetAllQuestions(string keyword);
        Task<Question> GetQuestionByIdAsync(int id);

        Task<bool> CreateQuestionAsync(QuestionForCreateDto create);

        Task<bool> UpdateQuestionAsync(Question question, QuestionForUpdateDto questionUpdate);

        Task<bool> DeleteQuestionAsync(int id);

        Task<Course> GetAllCoursesName();

        #endregion
        Task<List<QuestionForReturnViewModel>> GetQuestionByIdCourseViewModel(int idCourse);
        Task<double> GetPoint(List<int> questionId, List<int> listAnswer, string courseId, string userId);
        #region ViewModel

        #endregion
    }
}
