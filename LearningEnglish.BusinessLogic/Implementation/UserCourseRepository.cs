using LearningEnglish.BusinessLogic.Dtos.UserCourse;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace LearningEnglish.BusinessLogic.Implementation
{
    public class UserCourseRepository : IUserCourseRepository
    {
        private readonly DataContext _context;
        public UserCourseRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteUserCourseAsync(int id)
        {
            try
            {
                var usercourseInDb = await _context.UserCourses.FirstOrDefaultAsync(x => x.Id == id);
                if (usercourseInDb == null)
                    return false;

                _context.UserCourses.Remove(usercourseInDb);

                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<UserCourseForListDto> GetAllUserCourses(string keyword)
        {
            var s = (from user in _context.Users
                     from userCourse in _context.UserCourses
                     from course in _context.Courses
                     where (user.Id == userCourse.UserId && course.Id == userCourse.CourseId
                     && (keyword == null || user.UserName.ToUpper().Contains(keyword.ToUpper()) ||
                     course.Name.ToUpper().Contains(keyword.ToUpper()))
                     //&&(keyword==null || user.Id.ToString()==keyword.Trim()|| course.Id.ToString() == keyword.Trim() )
                     )
                     orderby userCourse.Id
                     select new UserCourseForListDto
                     {
                         Id = userCourse.Id,
                         UserName = user.UserName,
                         CourseName = course.Name
                     }

                     ).ToList();
            return s.GroupBy(x => new { x.CourseName, x.UserName }).Select(y => y.First()); 
        }

        public async Task<UserCourseForListDto> GetUserCourseByIdAsync(int id)
        {
            var s = await (from user in _context.Users
                           from userCourse in _context.UserCourses
                           from course in _context.Courses
                           where (user.Id == userCourse.UserId && course.Id == userCourse.CourseId && userCourse.Id == id)
                           orderby userCourse.Id
                           select new UserCourseForListDto
                           {
                               Id = userCourse.Id,
                               UserName = user.UserName,
                               CourseName = course.Name
                           }

                     ).FirstOrDefaultAsync();
            return s;
        }
    }
}
