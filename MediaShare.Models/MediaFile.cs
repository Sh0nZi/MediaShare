using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MediaShare.Models
{
    public class MediaFile
    {
        private ICollection<Comment> comments;

        private ICollection<Vote> votes;

        private ICollection<ApplicationUser> favouritedBy;

        public MediaFile()
        {
            this.comments = new HashSet<Comment>();
            this.votes = new HashSet<Vote>();
            this.favouritedBy = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(50000000)]     
        public byte[] Content { get; set; }
        
        public string Description { get; set; }

        [Required]
        public MediaType Type { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        
        [Required]
        public string AuthorId { get; set; }

        public byte[] Thumbnail { get; set; }

        public virtual ApplicationUser Author { get; set; }

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

        public virtual ICollection<ApplicationUser> FavouritedBy
        {
            get
            {
                return this.favouritedBy;
            }
            set
            {
                this.favouritedBy = value;
            }
        }
    }
}