using AutoMapper;
using LearningEnglish.BusinessLogic.Dtos.Lesson;
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
    public class LessonRepository : ILessonRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LessonRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateLessonAsync(LessonForCreateDto createLesson)
        {
            try
            {
                var lesson = _mapper.Map<Lesson>(createLesson);

                _context.Lessons.Add(lesson);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteLessonAsync(int id)
        {
            try
            {
                var lessonInDb = await _context.Lessons
                    .Include(y => y.Pronunciations)
                    .Include(y => y.Vocabularies)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (lessonInDb == null)
                    return false;

                if (lessonInDb.Pronunciations.Any())
                {
                    _context.Pronunciations.RemoveRange(lessonInDb.Pronunciations);
                }

                if (lessonInDb.Vocabularies.Any())
                {
                    _context.Vocabularies.RemoveRange(lessonInDb.Vocabularies);
                }

                _context.Lessons.Remove(lessonInDb);

                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Course> GetAllCoursesName()
        {
            return _context.Courses.AsEnumerable();
        }

        public IEnumerable<Lesson> GetAllLessons(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _context.Lessons
                    .Include(x => x.Course)
                    .Where(x =>
                        x.Course.Id.ToString() == keyword.Trim() ||
                        x.Name.ToUpper().Contains(keyword.ToUpper()) ||
                        x.Course.Name.ToUpper().Contains(keyword.ToUpper()) ||
                        x.Introduce.ToUpper().Contains(keyword.ToUpper()) ||
                        x.Type.ToUpper().Contains(keyword.ToUpper()))
                    .AsEnumerable();
            }
            return _context.Lessons.Include( x=> x.Course).AsEnumerable();
        }

        public async Task<Lesson> GetLessonById(int Id)
        {
            return await _context.Lessons
                .Include(x => x.Course)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Lesson> GetLessonByIdAsync(int id)
        {
            return await _context.Lessons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Lesson>> GetLessonsByCourseId(int CourseId)
        {
            var result = await (from x in _context.Lessons.Include(x => x.Course)
                                where (x.CourseId == CourseId)
                                select x).ToListAsync();

            return result;
        }

        public async Task<bool> UpdateLessonAsync(Lesson lesson, LessonForUpdateDto lessnUpdate)
        {
            try
            {
                _mapper.Map(lessnUpdate, lesson);

                _context.Lessons.Update(lesson);

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
