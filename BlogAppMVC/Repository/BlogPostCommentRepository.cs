using BlogAppMVC.Data;
using BlogAppMVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogAppMVC.Repository
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostCommentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            dbContext.BlogPostComments.Add(blogPostComment);
            await dbContext.SaveChangesAsync();
            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetByIdAsync(Guid blogPostId)
        {
            return await dbContext.BlogPostComments.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }

        
    }
}
