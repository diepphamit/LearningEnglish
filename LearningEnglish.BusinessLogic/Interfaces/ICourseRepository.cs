using LearningEnglish.BusinessLogic.ViewModels.Course;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<CourseForListViewModel>> GetCourses();

        Task<CourseForDetailViewModel> GetCourseById(int Id);
    }
}
