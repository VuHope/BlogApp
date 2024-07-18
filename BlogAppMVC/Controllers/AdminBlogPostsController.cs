using BlogAppMVC.Models.Domain;
using BlogAppMVC.Models.ViewModels;
using BlogAppMVC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogAppMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            this.tagRepository = tagRepository;
            this.blogPostRepository = blogPostRepository;
        }

        public async Task<IActionResult> List()
        {
            var blogPosts = await blogPostRepository.GetAllAsync();
            return View(blogPosts);
        }

        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAll();
            var model = new AddBlogPost
            {
                Tags = tags.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPost addBlogPost)
        {
            //Nhận dữ liệu từ form (addBlogPost) và gán vào blogPost
            var blogPost = new BlogPost
            {
                Heading = addBlogPost.Heading,
                PageTitle = addBlogPost.PageTitle,
                Content = addBlogPost.Content,
                ShortDescription = addBlogPost.ShortDescription,
                FeaturedImageUrl = addBlogPost.FeaturedImageUrl,
                UrlHandle = addBlogPost.UrlHandle,
                PublishedDate = addBlogPost.PublishedDate,
                Author = addBlogPost.Author,
                Visible = addBlogPost.Visible
            };

            var selectedTags = new List<Tag>();
            
            foreach (var selectedTagId in addBlogPost.SelectedTags)
            {
                // Vì SelectedTags là mảng string nên phải chuyển về Guid thì mới GetById được
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                // Lấy tag từ database theo Id
                var existingTag = await tagRepository.GetByIdAsync(selectedTagIdAsGuid);
                if (existingTag != null)
                {
                    // Nếu tag tồn tại thì thêm vào selectedTags
                    selectedTags.Add(existingTag);
                }
            }
            blogPost.Tag = selectedTags;
            await blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var existingBlogPost = await blogPostRepository.GetByIdAsync(id);
            var tagsDomainModel = await tagRepository.GetAll();
            if (existingBlogPost == null)
            {
                return NotFound();
            }
            var model = new UpdateBlogPost
            {
                Id = existingBlogPost.Id,
                Heading = existingBlogPost.Heading,
                PageTitle = existingBlogPost.PageTitle,
                Content = existingBlogPost.Content,
                ShortDescription = existingBlogPost.ShortDescription,
                FeaturedImageUrl = existingBlogPost.FeaturedImageUrl,
                UrlHandle = existingBlogPost.UrlHandle,
                PublishedDate = existingBlogPost.PublishedDate,
                Author = existingBlogPost.Author,
                Visible = existingBlogPost.Visible,

                Tags = tagsDomainModel.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                SelectedTags = existingBlogPost.Tag.Select(t => t.Id.ToString()).ToArray()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogPost updateBlogPost)
        {
            var blogPost = new BlogPost
            {
                Id = updateBlogPost.Id,
                Heading = updateBlogPost.Heading,
                PageTitle = updateBlogPost.PageTitle,
                Content = updateBlogPost.Content,
                ShortDescription = updateBlogPost.ShortDescription,
                FeaturedImageUrl = updateBlogPost.FeaturedImageUrl,
                UrlHandle = updateBlogPost.UrlHandle,
                PublishedDate = updateBlogPost.PublishedDate,
                Author = updateBlogPost.Author,
                Visible = updateBlogPost.Visible
            };
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in updateBlogPost.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetByIdAsync(selectedTagIdAsGuid);
                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            blogPost.Tag = selectedTags;
            var updatedBlog = await blogPostRepository.UpdateAsync(blogPost);
            if (updatedBlog != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(UpdateBlogPost updateBlogPost)
        {
            var deletedBlogPost = await blogPostRepository.DeleteAsync(updateBlogPost.Id);
            if (deletedBlogPost != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
