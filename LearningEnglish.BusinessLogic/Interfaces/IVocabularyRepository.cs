using LearningEnglish.BusinessLogic.Dtos.Vocabulary;
using LearningEnglish.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface IVocabularyRepository
    {
        #region Dto

        IEnumerable<Vocabulary> GetAllVocabularies(string keyword);
        Task<Vocabulary> GetVocabularyByIdAsync(int id);

        Task<bool> CreateVocabularyAsync(VocabularyForCreateDto createVocabulary);

        Task<bool> UpdateVocabularyAsync(Vocabulary vocabulary, VocabularyForUpdateDto vocabularyUpdate);

        Task<bool> DeleteVocabularyAsync(int id);

        IEnumerable<Lesson> GetAllLessonsName();

        #endregion
    }
}
