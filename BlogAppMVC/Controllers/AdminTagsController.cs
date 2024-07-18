using BlogAppMVC.Data;
using BlogAppMVC.Models.Domain;
using BlogAppMVC.Models.ViewModels;
using BlogAppMVC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogAppMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAll();
            return View(tags);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddTags addTags)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Name = addTags.Name,
                    DisplayName = addTags.DisplayName
                };
                await tagRepository.AddTagAsync(tag);
                return RedirectToAction("List");
            }
            return View(addTags);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var tag = await tagRepository.GetByIdAsync(id);
            if (tag != null)
            {
                var updateTags = new UpdateTags
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(updateTags);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTags updateTags, Guid id)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Id = updateTags.Id,
                    Name = updateTags.Name,
                    DisplayName = updateTags.DisplayName
                };
                await tagRepository.UpdateTagAsync(tag);
                return RedirectToAction("List");
            }
            return View();
        }

        public async Task<IActionResult> Delete(UpdateTags updateTags)
        {
            var deletedTag = await tagRepository.DeleteTagAsync(updateTags.Id);
            if (deletedTag != null)
            {
                // Success
                return RedirectToAction("List");
            }
            // Error
            return RedirectToAction("List", new {id = updateTags.Id});
        }
    }
}
