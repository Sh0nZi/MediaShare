namespace MediaShare.Web.Infrastructure.Helpers
{
    using System.IO;
    using System.Linq;
    using System.Web;

    using NReco.VideoConverter;

    public class ThumbnailExtractor : IThumbnailExtractor
    {
        private readonly FFMpegConverter converter;

        public ThumbnailExtractor()
        {
            converter = new FFMpegConverter();
        }

        public byte[] GetVideoThumbnail(byte[] content)
        {
            string path = HttpRuntime.AppDomainAppPath + "\\MediaFiles\\sample.mp4";
            
            System.IO.File.WriteAllBytes(path, content);

            using (MemoryStream stream = new MemoryStream())
            {
                this.converter.GetVideoThumbnail(path, stream, 100);
                
                return stream.ToArray();
            }
        }
        
        public byte[] GetAudioThumbnail()
        {
            string path = HttpRuntime.AppDomainAppPath + ".\\MediaFiles\\mp3.jpg";
            var content = System.IO.File.ReadAllBytes(path);
            return content.ToArray();
        }
    }
}