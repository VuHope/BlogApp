﻿using BlogAppMVC.Models.Domain;
using BlogAppMVC.Models.ViewModels;
using BlogAppMVC.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLike addLike)
        {
            if(addLike != null)
            {
                var blogPostLike = new BlogPostLike
                {
                    BlogPostId = addLike.BlogPostId,
                    UserId = addLike.UserId
                };
                var result = await blogPostLikeRepository.AddLike(blogPostLike);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("TotalLike/{blogPostId:Guid}")]
        public async Task<IActionResult> GetTotalLikeForBlog(Guid blogPostId)
        {
            var result = await blogPostLikeRepository.GetTotalLike(blogPostId);
            return Ok(result);
        }
    }
}
