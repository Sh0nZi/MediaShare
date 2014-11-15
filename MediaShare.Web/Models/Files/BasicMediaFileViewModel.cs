namespace MediaShare.Web.Models.Files
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MediaShare.Models;
    using MediaShare.Web.Infrastructure.Mapping;

    public class BasicMediaFileViewModel : IMapFrom<MediaFile>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
        
        [Required]
        public string AuthorId { get; set; }

        public ICollection<Vote> Votes { get; set; }

        
        [Required]
        public MediaType Type { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = 17;
                result = result * 23 + this.Id.GetHashCode();
                result = result * 23 + ((Title != null) ? this.Title.GetHashCode() : 0);
                result = result * 23 + this.DateCreated.GetHashCode();
                result = result * 23 + ((AuthorId != null) ? this.AuthorId.GetHashCode() : 0);
                result = result * 23 + ((Votes != null) ? this.Votes.GetHashCode() : 0);
                result = result * 23 + this.Type.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as BasicMediaFileViewModel;

            return this.Id == other.Id;
        }
    }
}