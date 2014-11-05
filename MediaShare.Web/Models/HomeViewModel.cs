using MediaShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaShare.Web.Models
{
    public class HomeViewModel
    {
        
        public ICollection<MediaFile> Top3VideoFiles { get; set; }

        public ICollection<MediaFile> Top3AudioFiles { get; set; }
    }
}