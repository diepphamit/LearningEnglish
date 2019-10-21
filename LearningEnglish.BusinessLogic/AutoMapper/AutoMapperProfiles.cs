using AutoMapper;
using LearningEnglish.BusinessLogic.Dtos.User;
using LearningEnglish.BusinessLogic.ViewModels.Course;
using LearningEnglish.DataAccess.Entities;

namespace LearningEnglish.BusinessLogic.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Dtos

            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForReturnDto>();
            CreateMap<UserForCreateDto, User>();
            CreateMap<UserForUpdateDto, User>().ForMember(x => x.Id, opt => opt.Ignore());

            #endregion

            #region View Models
            CreateMap<Course, CourseForListViewModel>();
            CreateMap<Course, CourseForDetailViewModel>();
            #endregion
        }
    }
}
