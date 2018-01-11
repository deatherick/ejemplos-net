using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVCPartialDemo.Models;

namespace MVCPartialDemo.Controllers
{
    public class MyController : Controller
    {
        // GET: My
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Students(RequestStudentsModel arguments)
        {
            if (ModelState.IsValid)
            {
                var arr = new List<string> {"CS Students", "IT Students", "Civil Students", "Mech Students"};
                return PartialView("_Students", arr.Take(arguments.Cantidad));
            }
            return View("Index", arguments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Schools()
        {
            string[] arr = { "Harvard", "Yale", "Oxford", "Stanford" };
            return PartialView("_Schools", arr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Error()
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Error definido por el servidor");
        }
    }
}