using BlogAppMVC.Models.Domain;

namespace BlogAppMVC.Repository
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLike(Guid blogPostId);
        Task<IEnumerable<BlogPostLike>> GetLikeForBlog(Guid blogPostId);
        Task<BlogPostLike> AddLike(BlogPostLike blogPostLike);
    }
}
