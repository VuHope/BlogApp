﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogAppMVC.Models.ViewModels
{
    public class UpdateBlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        //Display tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        // Collect tags
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
