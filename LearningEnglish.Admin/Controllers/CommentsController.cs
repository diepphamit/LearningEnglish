using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LearningEnglish.BusinessLogic.Core;
using LearningEnglish.BusinessLogic.Dtos.Comment;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LearningEnglish.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(
            ICommentRepository commentRepository,
            IMapper mapper,
            ILogger<CommentsController> logger
        )
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllComments(string keyword, int page = 1, int pageSize = 10)
        {
            try
            {
                var list = _commentRepository.GetComments(keyword);

                int totalCount = list.Count();

                var query = list.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                var response = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentForListDto>>(query);

                var paginationSet = new PaginationSet<CommentForListDto>()
                {
                    Items = response,
                    Total = totalCount,
                };

                return Ok(paginationSet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Có lỗi trong quá trình lấy dữ liệu", ex.ToString());

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound();

            return Ok(_mapper.Map<CommentForReturnDto>(comment));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var commentInDb = await _commentRepository.GetCommentByIdAsync(id);
            if (commentInDb == null)
                return NotFound(id);

            var result = await _commentRepository.DeleteCommentAsync(commentInDb.Id);
            if (!result)
                return BadRequest("Có lỗi trong quá trình xóa dữ liệu: ");

            return Ok();
        }
    }
}