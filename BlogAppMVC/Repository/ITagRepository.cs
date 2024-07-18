using BlogAppMVC.Models.Domain;

namespace BlogAppMVC.Repository
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAll();
        Task<Tag?> GetByIdAsync(Guid id);
        Task<Tag> AddTagAsync(Tag tag);
        Task<Tag?> UpdateTagAsync(Tag tag);
        Task<Tag?> DeleteTagAsync(Guid id);
    }
}
