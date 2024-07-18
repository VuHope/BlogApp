using BlogAppMVC.Models.ViewModels;
using BlogAppMVC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppMVC.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogsController(IBlogPostRepository blogPostRepository, IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
        }
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailViewModel();
            if (blogPost != null)
            {
                var totalLikes = await blogPostLikeRepository.GetTotalLike(blogPost.Id);
                blogDetailsViewModel = new BlogDetailViewModel
                {
                    Id = blogPost.Id,
                    Content = blogPost.Content,
                    PageTitle = blogPost.PageTitle,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Heading = blogPost.Heading,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Visible = blogPost.Visible,
                    Tag = blogPost.Tag,
                    TotalLikes = totalLikes
                    
                };
            }
            return View(blogDetailsViewModel);
        }
    }
}
