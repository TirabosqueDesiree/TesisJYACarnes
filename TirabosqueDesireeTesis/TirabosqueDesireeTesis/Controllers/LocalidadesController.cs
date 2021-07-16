using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TirabosqueDesireeTesis.Clases;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LocalidadesController : Controller
    {
        private CarniceriaContext db = new CarniceriaContext();

        // GET: Localidades
        public ActionResult Index()
        {
            var localidads = db.Localidads.Include(l => l.Provincia);
            return View(localidads.ToList());
        }

        // GET: Localidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidads.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            return View(localidad);
        }

        // GET: Localidades/Create
        public ActionResult Create()
        {
            ViewBag.IdProvincia = new SelectList(CombosHelpers.GetProvincias(), "IdProvincia", "Descripcion");
            return View();
        }

        // POST: Localidades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Localidads.Add(localidad);
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

            ViewBag.IdProvincia = new SelectList(CombosHelpers.GetProvincias(), "IdProvincia", "Descripcion", localidad.IdProvincia);
            return View(localidad);
        }

        // GET: Localidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidads.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProvincia = new SelectList(db.Provincias, "IdProvincia", "Descripcion", localidad.IdProvincia);
            return View(localidad);
        }

        // POST: Localidades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(localidad).State = EntityState.Modified;
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
            ViewBag.IdProvincia = new SelectList(CombosHelpers.GetProvincias(), "IdProvincia", "Descripcion", localidad.IdProvincia);
            return View(localidad);
        }

        // GET: Localidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Localidad localidad = db.Localidads.Find(id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            return View(localidad);
        }

        // POST: Localidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Localidad localidad = db.Localidads.Find(id);
            try
            {
                db.Localidads.Remove(localidad);
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
            return View(localidad);
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
