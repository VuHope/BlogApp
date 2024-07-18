using BlogAppMVC.Data;
using BlogAppMVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogAppMVC.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TagRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await dbContext.Tags.ToListAsync();
        }
        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            var existingTag = await dbContext.Tags.FindAsync(id);
            return existingTag;
        }
        public async Task<Tag> AddTagAsync(Tag tag)
        {
            await dbContext.Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteTagAsync(Guid id)
        {
            var existingTag = await dbContext.Tags.FindAsync(id);
            if (existingTag == null)
            {
                return null;
            }
            dbContext.Tags.Remove(existingTag);
            await dbContext.SaveChangesAsync();
            return existingTag;
        }

        public async Task<Tag?> UpdateTagAsync(Tag tag)
        {
            var existingTag = await dbContext.Tags.FindAsync(tag.Id);
            if (existingTag == null)
            {
                return null;
            }
            existingTag.Name = tag.Name;
            existingTag.DisplayName = tag.DisplayName;
            await dbContext.SaveChangesAsync();
            return existingTag;
        }
    }
}
