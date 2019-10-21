using AutoMapper;
using LearningEnglish.BusinessLogic.Dtos.User;
using LearningEnglish.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LearningEnglish.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            if (await _authRepository.CheckLogin(userForLoginDto))
            {
                var appUser = await _authRepository.GetUserByUserName(userForLoginDto.Username);
                var userToReturn = _mapper.Map<UserForListDto>(appUser);
                var accessToken = await _authRepository.GenerateJwtToken(appUser);

                return Ok(new
                {
                    access_token = accessToken,
                    user = userToReturn
                });
            }

            return Unauthorized();
        }
    }
}