using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaShare.Web.CustomResult
{
    using System.IO; 
    using System.Web.Hosting; 
    using System.Web.Mvc;

    public class AudioResult : ActionResult 
    { 
        /// <summary> 
        /// The below method will respond with the Video file 
        /// </summary> 
        /// <param name="context"></param> 
        public override void ExecuteResult(ControllerContext context) 
        { 
            //The File Path 
            var videoFilePath = HostingEnvironment.MapPath("~/MediaFiles/Audio/Avenged Sevenfold - Hail To The King.mp3"); 
            //The header information 
            context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=Avenged Sevenfold - Hail To The King.mp3"); 
            var file = new FileInfo(videoFilePath); 
            //Check the file exist,  it will be written into the response 
            if (file.Exists) 
            { 
                var stream = file.OpenRead(); 
                var bytesinfile = new byte[stream.Length]; 
                stream.Read(bytesinfile, 0, (int)file.Length); 
                context.HttpContext.Response.BinaryWrite(bytesinfile); 
            }
        }
    }
}