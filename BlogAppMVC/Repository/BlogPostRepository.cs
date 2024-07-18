using BlogAppMVC.Data;
using BlogAppMVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogAppMVC.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include("Tag").ToListAsync();
        }
        public Task<BlogPost?> GetByIdAsync(Guid id)
        {
            var existingBlogPost = dbContext.BlogPosts.Include("Tag").FirstOrDefaultAsync(bp => bp.Id == id);
            return existingBlogPost;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await dbContext.BlogPosts.Include("Tag").FirstOrDefaultAsync(bp => bp.Id == blogPost.Id);
            if(existingBlogPost == null)
                return null;
            existingBlogPost.Heading = blogPost.Heading;
            existingBlogPost.PageTitle = blogPost.PageTitle;
            existingBlogPost.Content = blogPost.Content;
            existingBlogPost.ShortDescription = blogPost.ShortDescription;
            existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
            existingBlogPost.UrlHandle = blogPost.UrlHandle;
            existingBlogPost.PublishedDate = blogPost.PublishedDate;
            existingBlogPost.Author = blogPost.Author;
            existingBlogPost.Visible = blogPost.Visible;
            existingBlogPost.Tag = blogPost.Tag;
            await dbContext.SaveChangesAsync();
            return existingBlogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(bp => bp.Id == id);
            if(existingBlogPost == null)
                return null;
            dbContext.BlogPosts.Remove(existingBlogPost);
            await dbContext.SaveChangesAsync();
            return existingBlogPost;
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            var existingBlogPost = await dbContext.BlogPosts.Include("Tag").FirstOrDefaultAsync(bp => bp.UrlHandle == urlHandle);
            return existingBlogPost;
        }
    }
}
