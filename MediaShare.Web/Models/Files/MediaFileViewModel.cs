﻿namespace MediaShare.Web.Models.Files
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations; 
    using MediaShare.Web.Infrastructure.Mapping;
    using MediaShare.Models;

    public class MediaFileViewModel : AdvancedMediaFileViewModel, IMapFrom<MediaFile>
    {
        [MaxLength(50000000)]     
        public string Content { get; set; }
        
        public string Description { get; set; }

        public string Thumbnail { get; set; }
        
        public ICollection<Comment> Comments { get; set; }
    }
}