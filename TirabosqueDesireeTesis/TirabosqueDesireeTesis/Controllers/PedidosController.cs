using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TirabosqueDesireeTesis.Clases;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Controllers
{
    [Authorize(Roles = "User")]
    public class PedidosController : Controller
    {
        private CarniceriaContext db = new CarniceriaContext();

        // GET: Pedidos
        public ActionResult Index()
        {
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var pedidos = db.Pedidos.Where(P => P.IdUsuario == user.IdUsuario).Include(p => p.Estado).Include(p => p.Usuario);
            return View(pedidos.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }


        public ActionResult Create()
        {
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.IdUsuario = new SelectList(CombosHelpers.GetUsuarios(), "IdUsuario", "UserName");
            var view = new VMPedido
            {
                FechaPedido = DateTime.Now,
                DetalleTemps = db.PedidoDetalleTemps.Where(x => x.UserName == User.Identity.Name).ToList(),
                IdUsuario=user.IdUsuario
            };
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMPedido view)
        {
            
            if (ModelState.IsValid)
            {
                var respuesta = MovimientosHelpers.NuevoPedido(view, User.Identity.Name);
                if (respuesta.Realizado)
                {
                    return RedirectToAction("MisPedidos");
                }
                ModelState.AddModelError(string.Empty, respuesta.Mensaje);
            }

            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.IdUsuario = new SelectList(CombosHelpers.GetUsuarios(), "IdUsuario", "UserName");
            view.DetalleTemps = db.PedidoDetalleTemps.Where(x => x.UserName == User.Identity.Name).ToList();
            return View(view);
        }

        public ActionResult AgregarProducto()
        {
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.IdProducto = new SelectList(MisPedidosHelpers.GetListaProductosComboAgregar(), "IdProducto", "Producto");
          
            return View();
        }

        [HttpPost]
        public ActionResult AgregarProducto(VMAgregarProducto vm)
        {
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

         

            if (ModelState.IsValid)
            {
                var detalle = db.PedidoDetalleTemps.Where(x => x.UserName == User.Identity.Name && x.IdProducto == vm.IdProducto).FirstOrDefault();
                if (detalle == null)
                {
                    var producto = db.Productoes.Find(vm.IdProducto);
                    detalle = new PedidoDetalleTemp
                    {
                        Descripcion = producto.Descripcion,
                        Precio = producto.Precio,
                        IdProducto = producto.IdProducto,
                        Cantidad = (int)vm.Quantity,
                        UserName = User.Identity.Name
                    };
                   
                    db.PedidoDetalleTemps.Add(detalle);
                }
                else
                {
                    detalle.Cantidad += (int)vm.Quantity;
                    db.Entry(detalle).State = EntityState.Modified;
                }

                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.IdProducto = new SelectList(CombosHelpers.GetProductos(), "IdProducto", "Descripcion");
            return View();
        }

        public ActionResult DeleteProducto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productoTemp = db.PedidoDetalleTemps.Where(x => x.UserName == User.Identity.Name && x.IdProducto == id).FirstOrDefault();
            if (productoTemp == null)
            {
                return HttpNotFound();
            }
            db.PedidoDetalleTemps.Remove(productoTemp);
            db.SaveChanges();
            return RedirectToAction("Create");
        }


        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.IdUsuario = new SelectList(CombosHelpers.GetUsuarios(), "IdUsuario", "UserName");
            return View(pedido);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.IdUsuario = new SelectList(CombosHelpers.GetUsuarios(), "IdUsuario", "UserName");
            return View(pedido);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MisPedidos()
        {
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var idUsuario = user.IdUsuario;

            var listaMisPedidos = MisPedidosHelpers.GetMisPedidos(idUsuario);

            var listaDetalles = MisPedidosHelpers.GetDTODetallePed(idUsuario);

            var view = new VMMipedido
            {
                DTOPed2 = listaMisPedidos,
                Detalle = listaDetalles,
            };
               

            return View(view);
        }

        public ActionResult VerPedido(int id)
        {
            var user = db.Usuarios.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            var idUsuario = user.IdUsuario;
            var idPedido = id;
            var listaDetalles = MisPedidosHelpers.GetDTODetallePedidoSeleccionado(idUsuario, id);

            return View(listaDetalles);
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
