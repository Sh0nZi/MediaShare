namespace MediaShare.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        public string Content { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public int FileId { get; set; }
    }
}