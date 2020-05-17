using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Core.Models
{
    public class Tag
    {
        public Tag()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "The {0} must be required ")]
        [StringLength(225, ErrorMessage = "The {0} max length is {1}"), MinLength(2, ErrorMessage = "The {0} max length is {1}")]
        public string Name { get; set; }

        [StringLength(225, ErrorMessage = "The {0} max length is {1}"), MinLength(2, ErrorMessage = "The {0} max length is {1}")]
        public string UrlSlug { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}