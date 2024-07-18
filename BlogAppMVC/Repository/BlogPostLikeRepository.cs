
using BlogAppMVC.Data;
using BlogAppMVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogAppMVC.Repository
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostLikeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<BlogPostLike> AddLike(BlogPostLike blogPostLike)
        {
            await dbContext.BlogPostLikes.AddAsync(blogPostLike);
            await dbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<int> GetTotalLike(Guid blogPostId)
        {
            return await dbContext.BlogPostLikes.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
