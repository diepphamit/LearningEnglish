using LearningEnglish.BusinessLogic.Dtos.Pronunciation;
using LearningEnglish.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface IPronunciationRepository
    {
        #region Dto

        IEnumerable<Pronunciation> GetAllPronunciations(string keyword);
        Task<Pronunciation> GetPronunciationByIdAsync(int id);

        Task<bool> CreatePronunciationAsync(PronunciationForCreateDto create);

        Task<bool> UpdatePronunciationAsync(Pronunciation pronunciation, PronunciationForUpdateDto pronunciationUpdate);

        Task<bool> DeletePronunciationAsync(int id);

        Task<Lesson> GetAllLessonsName();

        #endregion
    }
}
