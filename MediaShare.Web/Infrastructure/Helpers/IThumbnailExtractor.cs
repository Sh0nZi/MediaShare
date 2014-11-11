namespace MediaShare.Web.Infrastructure.Helpers
{
    public interface IThumbnailExtractor
    {
        byte[] GetVideoThumbnail(byte[] content);

        byte[] GetAudioThumbnail();
    }
}
