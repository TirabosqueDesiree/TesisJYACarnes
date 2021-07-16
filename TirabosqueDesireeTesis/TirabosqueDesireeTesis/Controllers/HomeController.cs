using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Controllers
{
    public class HomeController : Controller
    {
        private CarniceriaContext db = new CarniceriaContext();

        public ActionResult Index()
        {
            var user = db.Usuarios.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            return View(user);
        }

        public ActionResult TerminosYCondiciones()
        {
            

            return View();
        }

        public ActionResult Faq()
        {


            return View();
        }
        public ActionResult Mensaje()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Mensaje(Mensaje mensaje)
        {

            if (ModelState.IsValid)
            {
                db.Mensajes.Add(mensaje);
                db.SaveChanges();
               

                return RedirectToAction("Respuesta");
            }
            else
            {
                return View(mensaje);
            }
           
        }
        public ActionResult Respuesta()
        {


            return View();
        }
    }
}