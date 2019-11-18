using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.DataAccess.Data;
using LearningEnglish.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using LearningEnglish.BusinessLogic.Dtos.Achievement;
using LearningEnglish.BusinessLogic.ViewModels.Achievement;

namespace LearningEnglish.BusinessLogic.Implementation
{
    public class AchievementRepository : IAchievementRepository
    {
        private readonly DataContext _context;
        public AchievementRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteAchievementAsync(int id)
        {
            try
            {
                var achievementInDb = await _context.Achievements.FirstOrDefaultAsync(x => x.Id == id);
                if (achievementInDb == null)
                    return false;

                _context.Achievements.Remove(achievementInDb);

                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AchievementForListDto> GetAchievementByIdAsync(int id)
        {
            var s = await (from user in _context.Users
                     join userCourse in _context.UserCourses on user.Id equals userCourse.Id
                     join course in _context.Courses on userCourse.Id equals course.Id
                     join ach in _context.Achievements on userCourse.Id equals ach.Id
                     where (ach.Id == id && ach.UserId == user.Id)
                     orderby user.Id
                     select new AchievementForListDto
                     {
                         Id = ach.Id,
                         Point = ach.Point,
                         TestDate = ach.TestDate,
                         UserName = user.UserName,
                         CourseName = course.Name
                     }

                     ).FirstOrDefaultAsync();
            return s;
        }

        public List<AchievementForReturnViewModel> GetAchievementsByUserIdViewModel(int userId)
        {
            var s = (from user in _context.Users
                     join userCourse in _context.UserCourses on user.Id equals userCourse.UserId
                     join course in _context.Courses on userCourse.CourseId equals course.Id
                     join ach in _context.Achievements on course.Id equals ach.CourseId
                     where (ach.UserId == user.Id && ach.UserId == userId)
                     orderby user.Id
                     select new AchievementForReturnViewModel
                     {
                         Id = ach.Id,
                         Point = ach.Point,
                         TestDate = ach.TestDate,
                         CourseName = course.Name
                     }

                     ).Distinct().ToList();

            return s;
        }

        public IEnumerable<AchievementForListDto> GetAllAchievements(string keyword)
        {
            var s = (from user in _context.Users
                          join userCourse in _context.UserCourses on user.Id equals userCourse.UserId
                          join course in _context.Courses on userCourse.CourseId equals course.Id
                          join ach in _context.Achievements on course.Id equals ach.CourseId
                          where(ach.UserId == user.Id)
                          orderby user.Id
                          select new AchievementForListDto
                          {
                              Id = ach.Id,
                              Point = ach.Point,
                              TestDate = ach.TestDate,
                              UserName = user.UserName,
                              CourseName = course.Name
                          }

                     ).ToList();
        
            return s.GroupBy(x => x.Id).Select(y => y.First());
        }
    }
}
