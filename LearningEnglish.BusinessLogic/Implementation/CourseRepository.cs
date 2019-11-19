using AutoMapper;
using LearningEnglish.BusinessLogic.Dtos.Course;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.Course;
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
    public class CourceRepository : ICourseRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CourceRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateCouseAsync(CourseForCreateDto courseCreate)
        {
            try
            {
                var course = _mapper.Map<Course>(courseCreate);

                _context.Courses.Add(course);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                var courseInDb = await _context.Courses
                    .Include(y => y.Questions).ThenInclude(z => z.Answers)
                    .Include(x => x.Lessons).ThenInclude(y => y.Pronunciations)
                    .Include(x => x.Lessons).ThenInclude(y => y.Vocabularies)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (courseInDb == null)
                    return false;

                if (courseInDb.Lessons.Any())
                {
                    foreach (var lesson in courseInDb.Lessons)
                    {
                        

                        if (lesson.Pronunciations.Any())
                        {
                            _context.Pronunciations.RemoveRange(lesson.Pronunciations);
                        }

                        if (lesson.Vocabularies.Any())
                        {
                            _context.Vocabularies.RemoveRange(lesson.Vocabularies);
                        }

                        _context.Lessons.Remove(lesson);
                    }

                }

                if (courseInDb.Questions.Any())
                {
                    foreach (var question in courseInDb.Questions)
                    {


                        if (question.Answers.Any())
                        {
                            _context.Answers.RemoveRange(question.Answers);
                        }

                        _context.Questions.Remove(question);
                    }

                }

                if (courseInDb.Achievements.Any())
                {
                    foreach (var achievement in courseInDb.Achievements)
                    {
                        _context.Achievements.Remove(achievement);
                    }

                }

                _context.Courses.Remove(courseInDb);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CourseForDetailViewModel> GetCourseById(int Id)
        {
            var result = await _context.Courses.FirstOrDefaultAsync(x => x.Id == Id);

            return _mapper.Map<CourseForDetailViewModel>(result);
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CourseForListViewModel>> GetCourses()
        {
            var result = await _context.Courses.OrderByDescending(x => x.Id).ToListAsync();

            return _mapper.Map<List<CourseForListViewModel>>(result);
        }

        public IEnumerable<Course> GetCourses(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _context.Courses
                    .Where(x =>
                        x.Name.ToUpper().Contains(keyword.ToUpper()) ||
                        x.Introduce.ToUpper().Contains(keyword.ToUpper()))
                    .AsEnumerable();
            }
            return _context.Courses.AsEnumerable();
        }

        public async Task<List<CourseForListViewModel>> GetCoursesByLevelClass(int levalClass)
        {
            var result = await _context.Courses.Where(x=>x.LevelClass==levalClass).OrderByDescending(x => x.Id).ToListAsync();

            return _mapper.Map<List<CourseForListViewModel>>(result);
        }

        public async Task<List<CourseForListViewModel>> GetNewCourses()
        {
            var result = await _context.Courses.OrderByDescending(x => x.Id).Take(4).ToListAsync();

            return _mapper.Map<List<CourseForListViewModel>>(result);
        }

        public async Task<List<CourseForListViewModel>> GetPopularCourses()
        {
            var listUserCourse = _context.UserCourses.GroupBy(x => x.Course)
                  .OrderByDescending(g => g.Count())
                  .Select(g => g).ToList();
            listUserCourse = listUserCourse.Take(4).ToList();

            List<CourseForListViewModel> listReturn = new List<CourseForListViewModel>();
            foreach(var item in listUserCourse)
            {
                CourseForListViewModel course = _mapper.Map<CourseForListViewModel>(item.Key);
                listReturn.Add(course);
            }

            return listReturn;
        }

        public async Task<bool> UpdateCouseAsync(Course course, CourseForUpdateDto courseUpdate)
        {
            try
            {
                _mapper.Map(courseUpdate, course);

                _context.Courses.Update(course);

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
