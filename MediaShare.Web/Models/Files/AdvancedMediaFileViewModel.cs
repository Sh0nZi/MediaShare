namespace MediaShare.Web.Models.Files
{
    using MediaShare.Web.Infrastructure.Mapping;
    using MediaShare.Models;

    public class AdvancedMediaFileViewModel : BasicMediaFileViewModel, IMapFrom<MediaFile>
    {
        public int ViewsCount { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}