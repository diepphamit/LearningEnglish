using AutoMapper;
using LearningEnglish.BusinessLogic.Dtos.Question;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.BusinessLogic.ViewModels.Question;
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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public QuestionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateQuestionAsync(QuestionForCreateDto create)
        {
            try
            {
                var question = _mapper.Map<Question>(create);

                _context.Questions.Add(question);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            try
            {
                var questionInDb = await _context.Questions
                    .Include(y => y.Answers)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (questionInDb == null)
                    return false;

                if (questionInDb.Answers.Any())
                {
                    _context.Answers.RemoveRange(questionInDb.Answers);
                }

                _context.Questions.Remove(questionInDb);

                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Course> GetAllCoursesName()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetAllQuestions(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _context.Questions
                    .Include(x => x.Course)
                    .Where(x =>
                    x.Name.ToUpper().Contains(keyword.ToUpper()) ||
                    x.Course.Id.ToString() == keyword.Trim()
                    )
                    .AsEnumerable();
            }
            return _context.Questions.Include(x => x.Course).AsEnumerable();
        }

        public async Task<double> GetPoint(List<int> questionId, List<int> listAnswer, string courseId, string userId)
        {
            int totalQuestion = questionId.Count();
            int totalCorrect = 0;
            float points = 0;
            float pointPerQuestion = 10 / (float)totalQuestion;
            foreach (var qId in questionId)
            {
                List<Answer> answers = new  List<Answer>();
                answers = await _context.Answers.Where(x => x.QuestionId == qId).ToListAsync();
                int totalCorrectPerQuestion = 0;
                int totalCorrectPerQuestionForYou = 0;
                int totalWrongPerQuestionForYou = 0;
                foreach (var answer in answers)
                {
                    if (answer.CorrectAnswer) totalCorrectPerQuestion ++;

                    foreach(var answerId in listAnswer)
                    {
                        if(answer.Id == answerId)
                        {
                            if (answer.CorrectAnswer)
                            {
                                totalCorrect++;
                                totalCorrectPerQuestionForYou++;
                            }
                            else totalWrongPerQuestionForYou++;
                            break;

                        }
                    }
                }
                float pointPerOneRight = pointPerQuestion / (float)totalCorrectPerQuestion;
                float point = pointPerOneRight * (totalCorrectPerQuestionForYou - totalWrongPerQuestionForYou);
                if (point > 0) points += point;
            }
            UserCourse userCourse = new UserCourse()
            {
                CourseId = Convert.ToInt32(courseId),
                UserId = Convert.ToInt32(userId)
            };
            Achievement ach = new Achievement()
            {
                Point = points,
                TestDate = DateTime.Now,
                CourseId = Convert.ToInt32(courseId),
                UserId = Convert.ToInt32(userId)
            };
            try
            {
                _context.UserCourses.Add(userCourse);
                _context.Achievements.Add(ach);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Math.Round(points, 2);


        }

        public async Task<Question> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<QuestionForReturnViewModel>> GetQuestionByIdCourseViewModel(int idCourse)
        {
           
            var listForReturn = await _context.Questions.Include(x => x.Answers).Where(x => x.CourseId == idCourse).ToListAsync();
            Random rnd = new Random();
            listForReturn = listForReturn.OrderBy(x => rnd.Next()).ToList();
            int i = 0;
            foreach(var list in listForReturn)
            {
                listForReturn[i++].Answers = list.Answers.OrderBy(x => rnd.Next()).ToList();
            }
            return _mapper.Map<List<QuestionForReturnViewModel>>(listForReturn);
            
        }

        public async Task<bool> UpdateQuestionAsync(Question question, QuestionForUpdateDto questionUpdate)
        {
            try
            {
                _mapper.Map(questionUpdate, question);

                _context.Questions.Update(question);

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
