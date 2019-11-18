using AutoMapper;
using LearningEnglish.BusinessLogic.Dtos.Vocabulary;
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
    public class VocabularyRepository : IVocabularyRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public VocabularyRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateVocabularyAsync(VocabularyForCreateDto createVocabulary)
        {
            try
            {
                var vocabulary = _mapper.Map<Vocabulary>(createVocabulary);

                _context.Vocabularies.Add(vocabulary);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteVocabularyAsync(int id)
        {
            try
            {
                var vocabularyInDb = await _context.Vocabularies.FirstOrDefaultAsync(x => x.Id == id);
                if (vocabularyInDb == null)
                    return false;

                _context.Vocabularies.Remove(vocabularyInDb);

                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Lesson> GetAllLessonsName()
        {
            return _context.Lessons.AsEnumerable();
        }

        public IEnumerable<Vocabulary> GetAllVocabularies(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _context.Vocabularies
                    .Include(x => x.Lesson)
                    .Where(x =>
                    x.Lesson.Id.ToString() == keyword.Trim() ||
                    x.Lesson.Name.Trim().Contains(keyword.Trim())
                    )
                    .AsEnumerable();
            }
            return _context.Vocabularies.Include(x => x.Lesson).AsEnumerable();
        }

        public async Task<Vocabulary> GetVocabularyByIdAsync(int id)
        {
            return await _context.Vocabularies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateVocabularyAsync(Vocabulary vocabulary, VocabularyForUpdateDto vocabularyUpdate)
        {
            try
            {
                _mapper.Map(vocabularyUpdate, vocabulary);

                _context.Vocabularies.Update(vocabulary);

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
