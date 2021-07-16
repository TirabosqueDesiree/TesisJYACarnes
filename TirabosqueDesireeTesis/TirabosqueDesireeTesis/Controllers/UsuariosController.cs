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
    public class UsuariosController : Controller
    {
        private CarniceriaContext db = new CarniceriaContext();
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Localidad).Include(u => u.Provincia);
            return View(usuarios.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

      
        public ActionResult Create()
        {
            ViewBag.idLocalidad = new SelectList(CombosHelpers.GetLocalidades(), "IdLocalidad", "Descripcion");
            ViewBag.idProvincia = new SelectList(CombosHelpers.GetProvincias(), "IdProvincia", "Descripcion");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                    UserHelpers.CreateUserASP(usuario.UserName, "User");

                    if (usuario.ImagenFile != null)
                    {

                        var folder = "~/Content/Usuarios";
                        var file = string.Format("{0}.jpg", usuario.IdUsuario);
                        var respuesta = FilesHelpers.UploadPhoto(usuario.ImagenFile, folder, file);
                        if (respuesta)
                        {
                            var pic = string.Format("{0}/{1}", folder, file);
                            usuario.Imagen = pic;
                            db.Entry(usuario).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                    }
                    ViewBag.Mensaje = "Inicie sesión. Su contraseña es su dirección de correo.";
                    return RedirectToAction("LoginMensaje", "Account");
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

            ViewBag.idLocalidad = new SelectList(CombosHelpers.GetLocalidades(), "IdLocalidad", "Descripcion");
            ViewBag.idProvincia = new SelectList(CombosHelpers.GetProvincias(), "IdProvincia", "Descripcion");
            return View(usuario);
        }

        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.idLocalidad = new SelectList(CombosHelpers.GetLocalidades(), "IdLocalidad", "Descripcion");
            ViewBag.idProvincia = new SelectList(CombosHelpers.GetProvincias(), "IdProvincia", "Descripcion");
            return View(usuario);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    if (usuario.ImagenFile != null)
                    {
                        var pic = string.Empty;
                        var folder = "~/Content/Usuarios";
                        var file = string.Format("{0}.jpg", usuario.IdUsuario);
                        var respuesta = FilesHelpers.UploadPhoto(usuario.ImagenFile, folder, file);

                        if (respuesta)
                        {

                            pic = string.Format("{0}/{1}", folder, file);
                            usuario.Imagen = pic;
                        }
                    }
                    var db2 = new CarniceriaContext();
                    var currentUser = db2.Usuarios.Find(usuario.IdUsuario);
                    if (currentUser.UserName != usuario.UserName)
                    {
                        UserHelpers.UpdateUserName(currentUser.UserName, usuario.UserName);
                    }
                    db2.Dispose();

                    db.Entry(usuario).State = EntityState.Modified;

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
            ViewBag.idLocalidad = new SelectList(CombosHelpers.GetLocalidades(), "IdLocalidad", "Descripcion", usuario.IdLocalidad);
            ViewBag.idProvincia = new SelectList(CombosHelpers.GetProvincias(), "IdProvincia", "Descripcion", usuario.IdProvincia);
            return View(usuario);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuario = db.Usuarios.Find(id);
            try
            {
                db.Usuarios.Remove(usuario);
                db.SaveChanges();

                UserHelpers.DeleteUser(usuario.UserName, "User");

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
           
            return View(usuario);
        }
        public JsonResult GetLocalidades(int IdProvincia)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var localidades = db.Localidads.Where(l => l.IdProvincia == IdProvincia);
            return Json(localidades);
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
