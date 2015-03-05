namespace MediaShare.Web.Infrastructure.Helpers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using NReco.VideoConverter;
    using System;
    using System.Drawing;

    public class MediaHelper : IMediaHelper
    {
        private const string TempFirecotry = @"\MediaFiles\";

        private const int ThumbnailFrame = 100;

        private readonly FFMpegConverter converter;

        public MediaHelper()
        {
            converter = new FFMpegConverter();
        }

        public byte[] GetThumbnail(byte[] content = null)
        {
            if (content != null)
            {

                return this.GetVideoThumbnail(content);
            }
            else
            {
                return GetAudioThumbnail();
            }
        }

        private byte[] GetVideoThumbnail(byte[] content)
        {
            string path = HttpRuntime.AppDomainAppPath + TempFirecotry + "sample.mp4";

            File.WriteAllBytes(path, content);

            using (MemoryStream stream = new MemoryStream())
            {
                this.converter.GetVideoThumbnail(path, stream, ThumbnailFrame);

                return stream.ToArray();
            }
        }

        private byte[] GetAudioThumbnail()
        {
            string path = HttpRuntime.AppDomainAppPath + "." + TempFirecotry + "mp3.jpg";
            var content = File.ReadAllBytes(path);
            return content.ToArray();
        }

        static Size GetThumbnailSize(Image original)
        {
            // Maximum size of any dimension.
            const int maxPixels = 200;

            // Width and height.
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            // Compute best factor to scale entire image based on larger dimension.
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }

            // Return thumbnail size.
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }

    }
}