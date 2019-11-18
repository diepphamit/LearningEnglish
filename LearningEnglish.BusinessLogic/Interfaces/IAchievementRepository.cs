using LearningEnglish.BusinessLogic.Dtos.Achievement;
using LearningEnglish.BusinessLogic.ViewModels.Achievement;
using LearningEnglish.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface IAchievementRepository
    {
        #region Dto

        IEnumerable<AchievementForListDto> GetAllAchievements(string keyword);
        Task<AchievementForListDto> GetAchievementByIdAsync(int id);


        Task<bool> DeleteAchievementAsync(int id);

        #endregion

        #region ViewModel
        List<AchievementForReturnViewModel> GetAchievementsByUserIdViewModel(int userId);
        #endregion
    }
}
