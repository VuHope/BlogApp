using BlogAppMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogAppMVC.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext dbContext;

        public UserRepository(AuthDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var user = await dbContext.Users.ToListAsync();
            var superAdminUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");

            if(superAdminUser is not null)
            {
                user.Remove(superAdminUser);
            }
            return user;
        }
    }
}
