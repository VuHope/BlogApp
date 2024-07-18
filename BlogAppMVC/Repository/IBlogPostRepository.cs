using BlogAppMVC.Models.Domain;

namespace BlogAppMVC.Repository
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
