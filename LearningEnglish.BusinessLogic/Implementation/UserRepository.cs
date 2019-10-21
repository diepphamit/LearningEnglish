using AutoMapper;
using LearningEnglish.BusinessLogic.Dtos.User;
using LearningEnglish.BusinessLogic.Interfaces;
using LearningEnglish.DataAccess.Data;
using LearningEnglish.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglish.BusinessLogic.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserRepository(DataContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IEnumerable<User> GetListUsers(string keyword)
        {

            if (!string.IsNullOrEmpty(keyword))
            {
                return _context.Users
                     .Where(x =>
                         x.Email.ToLower().Contains(keyword.ToLower()) ||
                         x.UserName.ToLower().Contains(keyword.ToLower()) ||
                         x.FullName.ToLower().Contains(keyword.ToLower()) ||
                         x.PhoneNumber.ToLower().Contains(keyword.ToLower()))
                     .AsEnumerable();
            }

            return _context.Users.AsEnumerable();
        }

        public async Task<(bool Succeeded, string[] Errors)> CreateUserAsync(UserForCreateDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (!result.Succeeded)
                    return (false, result.Errors.Select(e => e.Description).ToArray());

                user = await _userManager.FindByNameAsync(user.UserName);

                result = await _userManager.AddToRolesAsync(user, userDto.Roles.Distinct());

                if (!result.Succeeded)
                {
                    await DeleteUserAsync(user);
                    return (false, result.Errors.Select(e => e.Description).ToArray());
                }

                return (true, new string[] { });
            }
            catch (Exception ex)
            {
                return (false, new string[] { ex.ToString() });
            }
        }

        public async Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);

            return (result.Succeeded, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }

        public async Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(int id, User userInDb, UserForUpdateDto userDto)
        {
            _mapper.Map(userDto, userInDb);

            var result = await _userManager.UpdateAsync(userInDb);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            if (userDto.Roles != null && userDto.Roles.Any())
            {
                var userRoles = await _userManager.GetRolesAsync(userInDb);

                var rolesToRemove = userRoles.Except(userDto.Roles).ToArray();
                var rolesToAdd = userDto.Roles.Except(userRoles).Distinct().ToArray();

                if (rolesToRemove.Any())
                {
                    result = await _userManager.RemoveFromRolesAsync(userInDb, rolesToRemove);
                    if (!result.Succeeded)
                        return (false, result.Errors.Select(e => e.Description).ToArray());
                }

                if (rolesToAdd.Any())
                {
                    result = await _userManager.AddToRolesAsync(userInDb, rolesToAdd);
                    if (!result.Succeeded)
                        return (false, result.Errors.Select(e => e.Description).ToArray());
                }
            }

            return (true, new string[] { });
        }
    }
}
