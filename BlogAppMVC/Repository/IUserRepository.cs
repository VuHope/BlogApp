using Microsoft.AspNetCore.Identity;

namespace BlogAppMVC.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
