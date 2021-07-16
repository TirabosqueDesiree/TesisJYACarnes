using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TirabosqueDesireeTesis.Clases;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Controllers
{
    public class OrdenEmpleadoController : Controller
    {
        private CarniceriaContext db = new CarniceriaContext();

        public ActionResult ListaPedidos()
        {
            var lista = OrdenEmpleadoHelpers.GetPedidosEmpleado();

            return View(lista);
        }


        public ActionResult ListaDetalle(int id)
        {
            var lista = OrdenEmpleadoHelpers.GetPedidoDetalle(id);

            foreach (var item in lista)
            {
                DTOPedidoDetalle d = new DTOPedidoDetalle
                {
                    IdPedido = item.IdPedido,
                    Fecha = item.Fecha,
                    Usuario = item.Usuario,
                    Producto = item.Producto,
                    Cantidad = item.Cantidad,
                    Precio = item.Precio,
                    IdEstado = item.IdEstado,
                    Estado = item.Estado,

                };

            }

            Session["IdPedido"] = id;



            return View(lista);
        }

        public ActionResult EditarEstado()
        {
            var idPedido = int.Parse(Session["IdPedido"].ToString());

            ViewBag.IdEstado = new SelectList(OrdenEmpleadoHelpers.GetEstados(), "IdEstado", "Descripcion");

            ViewBag.Pedido = idPedido;

            return View();
        }

        [HttpPost]
        public ActionResult EditarEstado(FormCollection form)
        {
            var idPedido = int.Parse(Session["IdPedido"].ToString());

            var idEstado = Convert.ToInt32(form["IdEstado"]);

            if (idEstado != 0)
            {
                OrdenEmpleadoHelpers.EditarEstadoPedido(idEstado, idPedido);
                var lista = OrdenEmpleadoHelpers.GetPedidosEmpleado();
                return RedirectToAction("ListaPedidos", lista);
            }
            else
            {
                ViewBag.Mensage = "Debe seleccionar un nuevo estado";
                var idPedido2 = int.Parse(Session["IdPedido"].ToString());

                ViewBag.IdEstado = new SelectList(OrdenEmpleadoHelpers.GetEstados(), "IdEstado", "Descripcion");

                ViewBag.Pedido = idPedido2;
            }
            

            return  View();
          

           
        }

    }
}




