using BlogAppMVC.Models.Domain;

namespace BlogAppMVC.Repository
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);
        Task<IEnumerable<BlogPostComment>> GetByIdAsync(Guid blogPostId);
    }
}
    