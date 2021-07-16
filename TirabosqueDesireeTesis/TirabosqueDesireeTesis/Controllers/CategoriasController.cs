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
    [Authorize(Roles ="Admin")]
    public class CategoriasController : Controller
    {
        private CarniceriaContext db = new CarniceriaContext();

        // GET: Categorias
        public ActionResult Index()
        {
            return View(db.Categorias.ToList());
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCategoria,Descripcion")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Categorias.Add(categoria);
                    db.SaveChanges();
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
               
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCategoria,Descripcion")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(categoria).State = EntityState.Modified;
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
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            try
            {
                db.Categorias.Remove(categoria);
                db.SaveChanges();
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
           
            return RedirectToAction("Index");
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
