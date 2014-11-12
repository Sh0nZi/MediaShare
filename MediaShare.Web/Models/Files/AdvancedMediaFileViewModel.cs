using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediaShare.Web.Infrastructure.Mapping;
using MediaShare.Models;

namespace MediaShare.Web.Models.Files
{
    public class AdvancedMediaFileViewModel : BasicMediaFileViewModel, IMapFrom<MediaFile>
    {
        public int ViewsCount { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}