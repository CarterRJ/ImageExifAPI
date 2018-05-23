using Audioagent_Image_API.Models;
using ExifLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Audioagent_Image_API.Controllers
{
    public class ImageController : ApiController
    {
        // GET api/values
        public ImageModel Get(string url)
        {
            Image img = DownloadData(url);
            return new ImageModel(img);
        }

        private Image DownloadData(string url)
        {
            Image img = null;
            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Timeout = 10 * 1000;
                WebResponse response = webRequest.GetResponse();
                Stream stream = response.GetResponseStream();
                img = Image.FromStream(stream);
                stream.Seek(0, SeekOrigin.Begin);
                using (ExifReader reader = new ExifReader(stream))
                {
                    // Extract the tag data using the ExifTags enumeration
                    DateTime datePictureTaken;
                    if (reader.GetTagValue<DateTime>(ExifTags.DateTimeDigitized,
                                                    out datePictureTaken))
                    {
                        // Do whatever is required with the extracted information
                    }
                    foreach (ExifTags tagNum in Enum.GetValues(typeof(ExifTags)).Cast<ExifTags>())
                    {
                        if (reader.GetTagValue<DateTime>(ExifTags.DateTimeDigitized,
                                                    out datePictureTaken))
                        {

                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return img;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }
    }
}
