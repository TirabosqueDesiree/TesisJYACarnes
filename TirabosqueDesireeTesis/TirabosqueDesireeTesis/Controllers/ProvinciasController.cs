using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProvinciasController : Controller
    {
        private CarniceriaContext db = new CarniceriaContext();

        // GET: Provincias
        public ActionResult Index()
        {
            return View(db.Provincias.ToList());
        }

        // GET: Provincias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincia provincia = db.Provincias.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        // GET: Provincias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Provincias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProvincia,Descripcion")] Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Provincias.Add(provincia);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    if (ex.InnerException != null &&
                     ex.InnerException.InnerException != null &&
                     ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un registro con dicho valor");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
               
            }

            return View(provincia);
        }

        // GET: Provincias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincia provincia = db.Provincias.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        // POST: Provincias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProvincia,Descripcion")] Provincia provincia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(provincia).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    if (ex.InnerException != null &&
                     ex.InnerException.InnerException != null &&
                     ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un registro con dicho valor");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
               
            }
            return View(provincia);
        }

        // GET: Provincias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provincia provincia = db.Provincias.Find(id);
            if (provincia == null)
            {
                return HttpNotFound();
            }
            return View(provincia);
        }

        // POST: Provincias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Provincia provincia = db.Provincias.Find(id);
            try
            {
                db.Provincias.Remove(provincia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                                   ex.InnerException.InnerException != null &&
                                   ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ModelState.AddModelError(string.Empty, "Le recordamos que no puede borrar este registro ya que contiene referencias a otros registros");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
           
            return View(provincia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
