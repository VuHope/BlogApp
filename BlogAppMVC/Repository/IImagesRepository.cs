namespace BlogAppMVC.Repository
{
    public interface IImagesRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
