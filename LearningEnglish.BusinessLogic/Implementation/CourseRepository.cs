using AutoMapper;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.Course;
using LearningEnglish.DataAccess.Data;
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
        public async Task<CourseForDetailViewModel> GetCourseById(int Id)
        {
            var result = await _context.Courses.FirstOrDefaultAsync(x => x.Id == Id);

            return _mapper.Map<CourseForDetailViewModel>(result);
        }

        public async Task<List<CourseForListViewModel>> GetCourses()
        {
            var result = await _context.Courses.OrderByDescending(x => x.Id).ToListAsync();

            return _mapper.Map<List<CourseForListViewModel>>(result);
        }
    }
}
