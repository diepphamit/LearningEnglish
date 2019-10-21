using LearningEnglish.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEnglish.DataAccess.Data
{
    public class DatabaseInitializer
    {
        private readonly DataContext _context;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;

        public DatabaseInitializer(DataContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            bool isProcess = false;

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new Role()
                {
                    Name = Constants.Constants.AdminRole
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name = Constants.Constants.TeacherRole
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name = Constants.Constants.CustomerRole
                });

                isProcess = true;
            }

            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new User()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                }, "123654$");

                var user = await _userManager.FindByNameAsync("admin");

                await _userManager.AddToRoleAsync(user, Constants.Constants.AdminRole);

                isProcess = true;
            }

            if (isProcess)
                await _context.SaveChangesAsync();
        }
    }

}
