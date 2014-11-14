namespace MediaShare.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public int MediaFileId { get; set; }

        public virtual MediaFile MediaFile { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}