namespace BlogAppMVC.Models.ViewModels
{
    public class AddLike
    {
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
