using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FA.JustBlog.Core.Models
{
    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "The {0} must be required ")]
        [StringLength(225), MinLength(2)]
        public string Title { get; set; }

        [Column("Short Description")]
        [MinLength(2, ErrorMessage = "The {0} max length is {1}")]
        public string ShortDescription { get; set; }

        [Column("Post Content")]
        public string PostContent { get; set; }

        [StringLength(1024)]
        public string UrlSlug { get; set; }

        public bool Publisher { get; set; }

        public DateTime PostedOn { get; set; }

        public DateTime? Modified { get; set; }

        public int? ViewCount { get; set; }

        public int? RateCount { get; set; }

        public int? TotalRate { get; set; }

        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public decimal Rate
        {
            get
            {
                if (RateCount == null || RateCount == 0 || TotalRate == null)
                {
                    return 0;
                }

                return Math.Round((decimal)TotalRate.Value / RateCount.Value, 2);
            }
        }
    }
}