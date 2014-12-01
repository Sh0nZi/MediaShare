using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MediaShare.Models
{
    public class MediaFile
    {
        private ICollection<Comment> comments;

        private ICollection<Vote> votes;

        public MediaFile()
        {
            this.comments = new HashSet<Comment>();
            this.votes = new HashSet<Vote>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }
   
        public string Content { get; set; }
        
        public string Description { get; set; }

        [Required]
        public MediaType Type { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        
        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
        
        public string Thumbnail { get; set; }

        public int ViewsCount { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
       
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }

        public virtual ICollection<Vote> Votes
        {
            get
            {
                return this.votes;
            }
            set
            {
                this.votes = value;
            }
        }
    }
}