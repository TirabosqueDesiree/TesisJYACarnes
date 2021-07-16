using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Clases
{
    public class MovimientosHelpers : IDisposable
    {
        private static CarniceriaContext db = new CarniceriaContext();


        public static Respuesta NuevoPedido(VMPedido view, string userName)
        {
            using (var transaccion = db.Database.BeginTransaction())
            {
                try
                {
                    var user = db.Usuarios.Where(u => u.UserName == userName).FirstOrDefault();
                    var pedido = new Pedido
                    {
                        IdUsuario = user.IdUsuario,
                        FechaPedido = view.FechaPedido,
                        Observacion = view.Observacion,
                        IdEstado = DBHelpers.GetEstado("Creado", db),
                    };
                    db.Pedidos.Add(pedido);
                    db.SaveChanges();
                    var detallesGuardados = db.PedidoDetalleTemps.Where(x => x.UserName == userName).ToList();


                    foreach (var detalle in detallesGuardados)
                    {
                        var pedidoDetalle = new DetallePedido
                        {
                            Descripcion = detalle.Descripcion,
                            IdPedido = pedido.IdPedido,
                            Precio = detalle.Precio,
                            IdProducto = detalle.IdProducto,
                            Cantidad = detalle.Cantidad,

                        };
                        db.DetallePedidos.Add(pedidoDetalle);
                        db.PedidoDetalleTemps.Remove(detalle);
                    }

                    db.SaveChanges();
                    transaccion.Commit();
                    return new Respuesta { Realizado = true };
                }
                catch (Exception ex)
                {

                    transaccion.Rollback();
                    return new Respuesta
                    {
                        Mensaje = ex.Message,
                        Realizado = false,
                    };
                }
                
            }
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}