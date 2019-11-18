using AutoMapper;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.DataAccess.Data;
using LearningEnglish.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Implementation
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CommentRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            try
            {
                var commentInDb = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
                if (commentInDb == null)
                    return false;

                _context.Comments.Remove(commentInDb);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Comment> GetComments(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _context.Comments
                    .Include(x => x.User)
                    .Where(x =>
                        x.Content.ToUpper().Contains(keyword.ToUpper()) ||
                        x.User.UserName.ToUpper().Contains(keyword.ToUpper())||
                        x.User.Id.ToString() == keyword)
                    .AsEnumerable();
            }
            return _context.Comments.Include(x => x.User).AsEnumerable();
        }
    }
}
