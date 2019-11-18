using AutoMapper;
using LearningEnglish.BusinessLogic.Dtos.Answer;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.Answer;
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
    public class AnswerRepository : IAnswerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AnswerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateAnswerAsync(AnswerForCreateDto create)
        {
            try
            {
                var answer = _mapper.Map<Answer>(create);

                _context.Answers.Add(answer);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteAnswerAsync(int id)
        {
            try
            {
                var answerInDb = await _context.Answers.FirstOrDefaultAsync(x => x.Id == id);
                if (answerInDb == null)
                    return false;

                _context.Answers.Remove(answerInDb);

                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Answer> GetAllAnswers(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _context.Answers
                    .Include(x => x.Question)
                    .Where(x =>
                    x.Question.Id.ToString() == keyword.Trim() ||
                    x.Name.ToUpper().Trim().Contains(keyword.ToUpper().Trim()) ||
                    x.Content.ToUpper().Trim().Contains(keyword.ToUpper().Trim())
                    )
                    .AsEnumerable();
            }
            return _context.Answers.Include(x => x.Question).AsEnumerable();
        }

        public Task<Lesson> GetAllLessonsName()
        {
            throw new NotImplementedException();
        }

        public async Task<Answer> GetAnswerByIdAsync(int id)
        {
            return await _context.Answers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<AnswerForReturnViewModel>> GetAnswerByIdQuestionViewModel(int idQuestion)
        {
            var listForReturn = await _context.Answers.Include(x=>x.Question).Where(x => x.QuestionId == idQuestion).ToListAsync();
            return _mapper.Map<List<AnswerForReturnViewModel>>(listForReturn);
        }

        public async Task<bool> UpdateAnswerAsync(Answer answer, AnswerForUpdateDto answerUpdate)
        {
            try
            {
                _mapper.Map(answerUpdate, answer);

                _context.Answers.Update(answer);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
