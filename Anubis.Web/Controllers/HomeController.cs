using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Anubis.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (WebSecurity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Applications");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Signup(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                ModelState.AddModelError("emailAddress", "Required");
            }

            if (ModelState.IsValid)
            {
                var body = new StringContent(string.Format("{{ \"email\": \"{0}\"}}", emailAddress), Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                await client.PostAsync("https://passverse.prefinery.com/api/v2/betas/3654/testers?api_key=Cq78siCqzM777fUfc11s", body);
                return RedirectToAction("Complete");
            }

            return View();
        }

        public ActionResult Complete()
        {
            return View();
        }
    }
}
