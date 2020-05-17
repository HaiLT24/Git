using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.UI.Areas.Admin.ViewModels
{
    public class PostAddViewModel
    {
        public PostAddViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(500, ErrorMessage = "The Title only a maximum of 500 characters")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(500, ErrorMessage = "The Short Description only a maximum of 500 characters")]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "The {0} must be required.")]
        [StringLength(50000, ErrorMessage = "The {0} only a maximum of {1} characters")]
        public string PostContent { get; set; }

        public bool Publisher { get; set; }

        [StringLength(255, ErrorMessage = "The {0} only a maximum of {1} characters")]
        [Display(Name = "Url Slug")]
        public string UrlSlug { get; set; }

        [Display(Name = "View Count")]
        public int? ViewCount { get; set; }

        [Display(Name = "Rate Count")]
        public int? RateCount { get; set; }

        [Display(Name = "Total Rate")]
        public int? TotalRate { get; set; }

        [Required(ErrorMessage = "The {0} must be required.")]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public virtual List<Tag> Tags { get; set; }

        [Display(Name = "Tag(s)")]
        public List<string> TagsId { get; set; }
    }
}