namespace MediaShare.Web.Infrastructure.Helpers
{
    public interface IMediaHelper
    {
        byte[] GetVideoThumbnail(byte[] content);

        byte[] GetAudioThumbnail();


    }
}