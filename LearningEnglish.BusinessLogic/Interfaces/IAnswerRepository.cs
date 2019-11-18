using LearningEnglish.BusinessLogic.Dtos.Answer;
using LearningEnglish.BusinessLogic.ViewModels.Answer;
using LearningEnglish.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface IAnswerRepository
    {
        #region Dto

        IEnumerable<Answer> GetAllAnswers(string keyword);
        Task<Answer> GetAnswerByIdAsync(int id);

        Task<bool> CreateAnswerAsync(AnswerForCreateDto create);

        Task<bool> UpdateAnswerAsync(Answer answer, AnswerForUpdateDto answerUpdate);

        Task<bool> DeleteAnswerAsync(int id);

        Task<Lesson> GetAllLessonsName();

        #endregion

        #region ViewModel
        Task<List<AnswerForReturnViewModel>> GetAnswerByIdQuestionViewModel(int idQuestion);
        #endregion
    }
}
