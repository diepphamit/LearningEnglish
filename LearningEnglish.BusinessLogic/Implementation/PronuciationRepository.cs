using AutoMapper;
using LearningEnglish.BusinessLogic.Dtos.Pronunciation;
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
    public class PronuciationRepository : IPronunciationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PronuciationRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreatePronunciationAsync(PronunciationForCreateDto create)
        {
            try
            {
                var pronunciation = _mapper.Map<Pronunciation>(create);

                _context.Pronunciations.Add(pronunciation);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeletePronunciationAsync(int id)
        {
            try
            {
                var pronunciationInDb = await _context.Pronunciations.FirstOrDefaultAsync(x => x.Id == id);
                if (pronunciationInDb == null)
                    return false;

                _context.Pronunciations.Remove(pronunciationInDb);

                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Lesson> GetAllLessonsName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pronunciation> GetAllPronunciations(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _context.Pronunciations
                    .Include(x => x.Lesson)
                    .Where(x =>
                    x.Lesson.Id.ToString() == keyword.Trim() ||
                    x.Lesson.Name.Trim().Contains(keyword.Trim())
                    )
                    .AsEnumerable();
            }
            return _context.Pronunciations.Include(x => x.Lesson).AsEnumerable();
        }

        public async Task<Pronunciation> GetPronunciationById(int Id)
        {
            return await _context.Pronunciations.Include(x => x.Lesson).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Pronunciation>> GetPronunciationsByLessonId(int LessonId)
        {
            return await (from x in _context.Pronunciations
                          where (x.LessonId == LessonId)
                          select x).ToListAsync();
        }

        public async Task<Pronunciation> GetPronunciationByIdAsync(int id)
        {
            return await _context.Pronunciations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdatePronunciationAsync(Pronunciation pronunciation, PronunciationForUpdateDto pronunciationUpdate)
        {
            try
            {
                _mapper.Map(pronunciationUpdate, pronunciation);

                _context.Pronunciations.Update(pronunciation);

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
