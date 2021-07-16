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
    public class ProductosController : Controller
    {
        private CarniceriaContext db = new CarniceriaContext();

        
        public ActionResult Index()
        {
            var productoes = db.Productoes.Include(p => p.Categoria).Include(p => p.Marca);
            return View(productoes.ToList());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.IdCategoria = new SelectList(CombosHelpers.GetCategorias(), "IdCategoria", "Descripcion");
            ViewBag.IdMarca = new SelectList(CombosHelpers.GetMarcas(), "IdMarca", "Descripcion");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Productoes.Add(producto);
                db.SaveChanges();
                if (producto.ImagenFile != null)
                {

                    var folder = "~/Content/Productos";
                    var file = string.Format("{0}.jpg", producto.IdProducto);
                    var respuesta = FilesHelpers.UploadPhoto(producto.ImagenFile, folder, file);
                    if (respuesta)
                    {
                        var pic = string.Format("{0}/{1}", folder, file);
                        producto.Imagen = pic;
                        db.Entry(producto).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                }

                return RedirectToAction("Index");
            }

            ViewBag.IdCategoria = new SelectList(CombosHelpers.GetCategorias(), "IdCategoria", "Descripcion");
            ViewBag.IdMarca = new SelectList(CombosHelpers.GetMarcas(), "IdMarca", "Descripcion");
            return View(producto);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategoria = new SelectList(CombosHelpers.GetCategorias(), "IdCategoria", "Descripcion");
            ViewBag.IdMarca = new SelectList(CombosHelpers.GetMarcas(), "IdMarca", "Descripcion");
            return View(producto);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                if (producto.ImagenFile != null)
                {
                    var pic = string.Empty;
                    var folder = "~/Content/Productos";
                    var file = string.Format("{0}.jpg", producto.IdProducto);
                    var respuesta = FilesHelpers.UploadPhoto(producto.ImagenFile, folder, file);

                    if (respuesta)
                    {

                        pic = string.Format("{0}/{1}", folder, file);
                        producto.Imagen = pic;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.IdCategoria = new SelectList(CombosHelpers.GetCategorias(), "IdCategoria", "Descripcion");
            ViewBag.IdMarca = new SelectList(CombosHelpers.GetMarcas(), "IdMarca", "Descripcion");
            return View(producto);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Productoes.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Productoes.Find(id);
            try
            {
                db.Productoes.Remove(producto);
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

            return View(producto);
        }


        public JsonResult GetMarcas(int Idcategoria)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var marcas = db.Marcas.Where(m => m.IdCategoria == Idcategoria);
            return Json(marcas);
        }


        public ActionResult VerPollos()
        {
            var lista = ListadosProdHelpers.GetListadoPollos();

            return View(lista);
        }
        public ActionResult VerPescados()
        {
            var lista = ListadosProdHelpers.GetListadoPescados();

            return View(lista);
        }
        public ActionResult VerCarneVacuna()
        {
            var lista = ListadosProdHelpers.GetListadoCarneVacuna();

            return View(lista);
        }
        public ActionResult VerCarneCerdo()
        {
            var lista = ListadosProdHelpers.GetListadoCarneCerdo();

            return View(lista);
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
