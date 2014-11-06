using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MediaShare.Web.Helpers
{
    public static class ThumbnailExtractor
    {
        private static FFMpegConverter converter;

        static ThumbnailExtractor()
        {
            converter = new FFMpegConverter();
        }

        internal static byte[] GetVideoThumbnail(byte[] content)
        {
            string path = HttpRuntime.AppDomainAppPath + "\\MediaFiles\\sample.mp4";
            
            System.IO.File.WriteAllBytes(path, content);

            using (MemoryStream stream = new MemoryStream())
            {
                converter.GetVideoThumbnail(path, stream, 100);
                
                return stream.ToArray();
            }
        }
        
        internal static byte[] GetAudioThumbnail()
        {
            string path = HttpRuntime.AppDomainAppPath + ".\\MediaFiles\\mp3.jpg";
            var content = System.IO.File.ReadAllBytes(path);
            return content.ToArray();
        }
    }
}