using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Clases
{

    public class OrdenEmpleadoHelpers : IDisposable
    {
        private static CarniceriaContext db = new CarniceriaContext();

        

        public static List<DTOPedido> GetPedidosEmpleado()
        {
            string cadenaCon = "Data Source=desiree;Initial Catalog=DesireeCarniceria;Integrated Security=True";

            var lista = new List<DTOPedido>();
            var sql = @"SELECT p.IdPedido, p.FechaPedido, u.Apellido + ' '+ u.Nombre as Usuario, p.Observacion, e.Descripcion,  sum(d.Precio*d.Cantidad) Total
                        from Pedidoes p, Usuarios u, DetallePedidoes d,  Estadoes e
                        where p.IdUsuario=u.idUsuario and d.IdPedido=p.IdPedido and e.IdEstado=p.IdEstado
                        group by p.IdPedido, p.FechaPedido, u.Apellido, u.Nombre, p.Observacion, e.Descripcion";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOPedido p = new DTOPedido();
                    p.IdPedido = (int)dr["idPedido"];
                    p.Fecha = (DateTime)dr["FechaPedido"];
                    p.Usuario = dr["Usuario"].ToString();
                    p.Observacion = dr["Observacion"].ToString();
                    p.Estado = dr["Descripcion"].ToString();
                    p.Total = (decimal)dr["Total"];

                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }

        public static List<DTOPedidoDetalle> GetPedidoDetalle(int IdPedido)
        {
            string cadenaCon = "Data Source=desiree;Initial Catalog=DesireeCarniceria;Integrated Security=True";

            var lista = new List<DTOPedidoDetalle>();
            var sql = @"SELECT p.IdPedido, p.FechaPedido, u.Apellido + ' '+ u.Nombre as Usuario, d.Descripcion as Producto, d.Cantidad, d.Precio, e.IdEstado, e.Descripcion
                        from Pedidoes p, Usuarios u, DetallePedidoes d, Estadoes e
                        where p.IdUsuario=u.idUsuario and d.IdPedido=p.IdPedido and p.IdEstado=e.IdEstado
						and d.IdPedido= @IdPedido";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
            cmd.Parameters.AddWithValue("@IdPedido", IdPedido);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOPedidoDetalle p = new DTOPedidoDetalle();
                    p.IdPedido = (int)dr["idPedido"];
                    p.Fecha = (DateTime)dr["FechaPedido"];
                    p.Usuario = dr["Usuario"].ToString();
                    p.Producto = dr["Producto"].ToString();
                    p.Cantidad = (int)dr["Cantidad"];
                    p.Precio = (decimal)dr["Precio"];
                    p.IdEstado = (int)dr["IdEstado"];
                    p.Estado = dr["Descripcion"].ToString();

                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }

        //public void EditarEstadoPedido(Pedido p, int idEstado)
        //{
        //    string cadenaCon = "Data Source=desiree;Initial Catalog=DesireeCarniceria;Integrated Security=True";

        //    var sql = "UPDATE Pedidoes SET IdEstado=@idEstado WHERE IdPedido=@idPedido";
        //    SqlConnection conex = new SqlConnection(cadenaCon);
        //    conex.Open();
        //    SqlCommand cmd = new SqlCommand(sql, conex);
        //    cmd.Parameters.AddWithValue("@idEstado", p.IdEstado);
           

        //    cmd.ExecuteNonQuery();
        //    conex.Close();

        //}
        public static List<Estado> GetEstados()
        {

            string cadenaCon = "Data Source=desiree;Initial Catalog=DesireeCarniceria;Integrated Security=True";

            var lista = new List<Estado>();
            var sql = @"select IdEstado, Descripcion
                        from Estadoes
                        where Descripcion Not Like '%Creado%'
                        order by Descripcion";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    Estado p = new Estado();
                    p.IdEstado = (int)dr["IdEstado"];
                    p.Descripcion = dr["Descripcion"].ToString();
                   

                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            lista.Add(new Estado
            {
                IdEstado = 0,
                Descripcion = "[Seleccione un estado...]"
            });
            return lista.OrderBy(x => x.Descripcion).ToList();


        }

        public static void EditarEstadoPedido(int idEstado, int idPedido)
        {
            string cadenaCon = "Data Source=desiree;Initial Catalog=DesireeCarniceria;Integrated Security=True";

            var sql = "UPDATE Pedidoes SET IdEstado=@IdEstado WHERE IdPedido= @IdPedido";
            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
            cmd.Parameters.AddWithValue("@IdEstado", idEstado);
            cmd.Parameters.AddWithValue("@IdPedido", idPedido);



            cmd.ExecuteNonQuery();
            conex.Close();

        }




        public void Dispose()
        {
            db.Dispose();
        }
    }
}