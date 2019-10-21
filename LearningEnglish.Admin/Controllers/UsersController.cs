using AutoMapper;
using LearningEnglish.BusinessLogic.Core;
using LearningEnglish.BusinessLogic.Dtos.User;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.DataAccess.Constants;
using LearningEnglish.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglish.Admin.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Constants.AdminRole)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<UsersController> logger
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllUsers(string keyword, int page, int pageSize)
        {
            try
            {
                var currentUser = HttpContext.User.Identity.Name;
                var list = _userRepository.GetListUsers(keyword);
                list = list.Where(x => x.UserName != currentUser);

                int totalCount = list.Count();

                var query = list.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);
                var response = _mapper.Map<IEnumerable<User>, IEnumerable<UserForListDto>>(query);

                var paginationSet = new PaginationSet<UserForListDto>()
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
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(_mapper.Map<UserForReturnDto>(user));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserForCreateDto input)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.CreateUserAsync(input);
                if (result.Succeeded)
                {
                    return Ok();
                }

                AddError(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDto input)
        {
            if (ModelState.IsValid)
            {
                var userInDb = await _userRepository.GetUserByIdAsync(id);
                if (userInDb == null)
                    return NotFound(id);

                var result = await _userRepository.UpdateUserAsync(id, userInDb, input);
                if (result.Succeeded)
                {
                    return Ok();
                }

                AddError(result.Errors);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userInDb = await _userRepository.GetUserByIdAsync(id);
            if (userInDb == null)
                return NotFound(id);

            var result = await _userRepository.DeleteUserAsync(userInDb);
            if (!result.Succeeded)
                return BadRequest("Có lỗi trong quá trình xóa tài khoản: " + string.Join(", ", result.Errors));

            return Ok();
        }

        private void AddError(IEnumerable<string> errors, string key = "")
        {
            foreach (var error in errors)
            {
                AddError(error, key);
            }
        }

        private void AddError(string error, string key = "")
        {
            ModelState.AddModelError(key, error);
        }
    }
}