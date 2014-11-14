namespace MediaShare.Web.Infrastructure.Helpers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using NReco.VideoConverter;

    public class MediaHelper : IMediaHelper
    {
        private const string TempFirecotry = @"\MediaFiles\";

        private const int ThumbnailFrame = 100;

        private readonly FFMpegConverter converter;
       
        public MediaHelper()
        {
            converter = new FFMpegConverter();
        }

        public byte[] GetVideoThumbnail(byte[] content)
        {
            string path = HttpRuntime.AppDomainAppPath + TempFirecotry + "sample.mp4";
            
            File.WriteAllBytes(path, content);

            using (MemoryStream stream = new MemoryStream())
            {
                this.converter.GetVideoThumbnail(path, stream, ThumbnailFrame);
                
                return stream.ToArray();
            }
        }
        
        public byte[] GetAudioThumbnail()
        {
            string path = HttpRuntime.AppDomainAppPath + "." + TempFirecotry + "mp3.jpg";
            var content = File.ReadAllBytes(path);
            return content.ToArray();
        }

    }
}