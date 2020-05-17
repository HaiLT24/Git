using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Core.Models
{
    public class Category
    {
        public Category()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        [Required(ErrorMessage = "The {0} must be required ")]
        [StringLength(225), MinLength(2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} must be required ")]
        [StringLength(225), MinLength(2)]
        public string UrlString { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}