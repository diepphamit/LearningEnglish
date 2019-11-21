using AutoMapper;
using LearningEnglish.BusinessLogic.Dtos.Achievement;
using LearningEnglish.BusinessLogic.Dtos.Answer;
using LearningEnglish.BusinessLogic.Dtos.Comment;
using LearningEnglish.BusinessLogic.Dtos.Course;
using LearningEnglish.BusinessLogic.Dtos.Lesson;
using LearningEnglish.BusinessLogic.Dtos.Pronunciation;
using LearningEnglish.BusinessLogic.Dtos.Question;
using LearningEnglish.BusinessLogic.Dtos.User;
using LearningEnglish.BusinessLogic.Dtos.Vocabulary;
using LearningEnglish.BusinessLogic.ViewModels.Achievement;
using LearningEnglish.BusinessLogic.ViewModels.Answer;
using LearningEnglish.BusinessLogic.ViewModels.Course;
using LearningEnglish.BusinessLogic.ViewModels.Lesson;
using LearningEnglish.BusinessLogic.ViewModels.Pronunciation;
using LearningEnglish.BusinessLogic.ViewModels.Question;
using LearningEnglish.BusinessLogic.ViewModels.User;
using LearningEnglish.BusinessLogic.ViewModels.Vocabulary;
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
            CreateMap<UserForUpdateFullDto, User>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<UserFullDto, User>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<UserForUpdateViewModel, User>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<User, UserForUpdateViewModel>();
            CreateMap<UserForCreateViewModel, User>();

            CreateMap<Course, CourseForListDto>();
            CreateMap<Course, CourseNameDto>();
            CreateMap<Course, CourseForReturnDto>();
            CreateMap<CourseForCreateDto, Course>();
            CreateMap<CourseForUpdateDto, Course>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Lesson, LessonForListDto>().ForMember(x => x.CourseName, y => { y.MapFrom(z => z.Course.Name); });
            CreateMap<Lesson, LessonForReturnDto>();
            CreateMap<Lesson, LessonNameDto>();
            CreateMap<LessonForCreateDto, Lesson>();
            CreateMap<LessonForUpdateDto, Lesson>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Vocabulary, VocabularyForListDto>().ForMember(x => x.LessonName, y => { y.MapFrom(z => z.Lesson.Name); });
            CreateMap<Vocabulary, VocabularyForReturnDto>();
            CreateMap<Vocabulary, VocabularyForReturnDto>();
            CreateMap<VocabularyForCreateDto, Vocabulary>();
            CreateMap<VocabularyForUpdateDto, Vocabulary>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Pronunciation, PronunciationForListDto>().ForMember(x => x.LessonName, y => { y.MapFrom(z => z.Lesson.Name); });
            CreateMap<Pronunciation, PronunciationForReturnDto>();
            CreateMap<PronunciationForCreateDto, Pronunciation>();
            CreateMap<PronunciationForUpdateDto, Pronunciation>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Answer, AnswerForListDto>().ForMember(x => x.QuestionName, y => { y.MapFrom(z => z.Question.Name); });
            CreateMap<Answer, AnswerForReturnDto>();
            CreateMap<AnswerForCreateDto, Answer>();
            CreateMap<AnswerForUpdateDto, Answer>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Question, QuestionForListDto>().ForMember(x => x.CourseName, y => { y.MapFrom(z => z.Course.Name); });
            CreateMap<Question, QuestionForReturnDto>();
            CreateMap<Question, QuestionNameDto>();
            CreateMap<QuestionForCreateDto, Question>();
            CreateMap<QuestionForUpdateDto, Question>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Comment, CommentForListDto>().ForMember(x => x.UserName, y => { y.MapFrom(z => z.User.UserName); });
            CreateMap<Comment, CommentForReturnDto>().ForMember(x => x.UserName, y => { y.MapFrom(z => z.User.UserName); });

            CreateMap<Achievement, AchievementForListDto>().ForMember(x => x.CourseName, y => { y.MapFrom(z => z.Course.Name); });

            #endregion

            #region View Models
            CreateMap<Course, CourseForListViewModel>();
            CreateMap<Course, CourseForDetailViewModel>();

            CreateMap<Lesson, LessonForListViewModel>().ForMember(x => x.CourseName, y => { y.MapFrom(z => z.Course.Name); }).ForMember(x => x.CourseIntroduce, y => { y.MapFrom(z => z.Course.Introduce); });
            CreateMap<Lesson, LessonForDetailViewModel>().ForMember(x => x.CourseName, y => { y.MapFrom(z => z.Course.Name); });

            CreateMap<Answer, AnswerForReturnViewModel>();
            CreateMap<Question, QuestionForReturnViewModel>();

            CreateMap<Achievement, AchievementForReturnViewModel>().ForMember(x => x.CourseName, y => { y.MapFrom(z => z.Course.Name); });
            CreateMap<Vocabulary, VocabularyForListViewModel>();
            CreateMap<Vocabulary, VocabularyForDetailViewModel>().ForMember(x => x.LessonName, y => { y.MapFrom(z => z.Lesson.Name); });

            CreateMap<Pronunciation, PronunciationForListViewModel>();
            CreateMap<Pronunciation, PronunciationForDetailViewModel>().ForMember(x => x.LessonName, y => { y.MapFrom(z => z.Lesson.Name); });

            #endregion
        }
    }
}
