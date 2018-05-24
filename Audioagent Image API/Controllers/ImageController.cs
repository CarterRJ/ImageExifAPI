using Audioagent_Image_API.Models;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web.Http;

namespace Audioagent_Image_API.Controllers
{
    public class ImageController : ApiController
    {
        // GET api/image?url
        public ImageModel Get(string url)
        {
            Image img = DownloadImage(url);
            var imageModel = new ImageModel(img);
            imageModel.Url = url;
            return imageModel;
        }

        private Image DownloadImage(string url)
        {
            Image img = null;
            //try
            //{
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Timeout = 10 * 1000;
                WebResponse response = webRequest.GetResponse();
                Stream stream = response.GetResponseStream();
                img = Image.FromStream(stream);
            //}
            // Either catch exceptions, fail silently and send empty ImageModel 
            // Or don't catch and send error code.
            //catch (Exception e)
            //{

            //}

            return img;
        }
    }
}
