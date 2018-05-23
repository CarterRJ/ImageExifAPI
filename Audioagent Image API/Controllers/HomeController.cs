using Audioagent_Image_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Audioagent_Image_API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Image(string url)
        {
            var client = new HttpClient();
            var response = client.GetAsync(Url.Action("Get", "api/image", new { url } , Request.Url.Scheme)).Result;
            var image = response.Content.ReadAsAsync<ImageModel>().Result;
            return PartialView("_Image", image);
        }
    }
}
