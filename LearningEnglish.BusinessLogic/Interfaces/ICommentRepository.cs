using LearningEnglish.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface ICommentRepository
    {
        #region Dto
        IEnumerable<Comment> GetComments(string keyword);
        Task<Comment> GetCommentByIdAsync(int id);

        Task<bool> DeleteCommentAsync(int id);

        #endregion 
    }
}