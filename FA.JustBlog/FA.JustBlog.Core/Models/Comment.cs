using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.Core.Models
{
    public class Comment
    {
        public Comment()
        {
            CommentId = Guid.NewGuid().ToString();
        }
        public string CommentId { get; set; }

        [Required(ErrorMessage = "The {0} must be required ")]
        [StringLength(225), MinLength(2)]
        public string Name { get; set; }

        [StringLength(225, ErrorMessage = "The {0} max length is {1}"), MinLength(2, ErrorMessage = "The {0} max length is {1}")]
        public string CommentHeader { get; set; }

        [StringLength(225, ErrorMessage = "The {0} max length is {1}"), MinLength(2, ErrorMessage = "The {0} max length is {1}")]
        public string CommentText { get; set; }

        public DateTime CommentTime { get; set; }

        public string PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}