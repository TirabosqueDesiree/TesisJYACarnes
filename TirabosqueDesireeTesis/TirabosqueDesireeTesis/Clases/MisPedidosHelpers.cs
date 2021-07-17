using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Clases
{
    public class MisPedidosHelpers
    {
        private static string cadenaCon = "Data Source=desiree;Initial Catalog=DesireeCarniceria;Integrated Security=True";
        private static CarniceriaContext db = new CarniceriaContext();

        public static List<DTOPed2> GetMisPedidos(int id)
        {


            var lista = new List<DTOPed2>();
            var sql = @"SELECT p.IdPedido, p.FechaPedido, u.Apellido + ' '+ u.Nombre as Usuario, e.Descripcion,  sum(d.Precio*d.Cantidad) Total
                        from Pedidoes p, Usuarios u, DetallePedidoes d,  Estadoes e
                        where p.IdUsuario=u.idUsuario and d.IdPedido=p.IdPedido and e.IdEstado=p.IdEstado
                        and p.IdUsuario=@IdUsuario
                        group by p.IdPedido, p.FechaPedido, u.Apellido, u.Nombre, e.Descripcion";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
            cmd.Parameters.AddWithValue("@IdUsuario", id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOPed2 p = new DTOPed2();
                    p.IdPedido = (int)dr["idPedido"];
                    p.Fecha = (DateTime)dr["FechaPedido"];
                    p.Usuario = dr["Usuario"].ToString();
                    p.Estado = dr["Descripcion"].ToString();
                    p.Total = (decimal)dr["Total"];

                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }

        internal static object GetListaPedidosXfecha(DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public static List<DTODetallePed> GetDTODetallePed(int id)
        {


            var lista = new List<DTODetallePed>();
            var sql = @"SELECT p.IdPedido, p.FechaPedido, d.Descripcion, d.Cantidad, d.Precio
                        from Pedidoes p, Usuarios u, DetallePedidoes d,  Estadoes e
                        where p.IdUsuario=u.idUsuario and d.IdPedido=p.IdPedido and e.IdEstado=p.IdEstado
						and p.IdUsuario=@IdUsuario
                        group by p.IdPedido, p.FechaPedido, d.Descripcion, d.Cantidad, d.Precio";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
            cmd.Parameters.AddWithValue("@IdUsuario", id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTODetallePed p = new DTODetallePed();
                    p.IdPedido = (int)dr["idPedido"];
                    p.Fecha = (DateTime)dr["FechaPedido"];
                    p.Producto = dr["Descripcion"].ToString();
                    p.Cantidad = (int)dr["Cantidad"];
                    p.Precio = (decimal)dr["Precio"];
                   

                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }
        public static List<DTODetallePed> GetDTODetallePedidoSeleccionado(int id, int idPedido)
        {


            var lista = new List<DTODetallePed>();
            var sql = @"SELECT p.IdPedido, p.FechaPedido, d.Descripcion, m.Descripcion as Marca, d.Cantidad, d.Precio
                        from Pedidoes p, Usuarios u, DetallePedidoes d,  Estadoes e, Productoes pr, Marcas m
                        where p.IdUsuario=u.idUsuario and d.IdPedido=p.IdPedido and e.IdEstado=p.IdEstado and d.IdProducto=pr.IdProducto
						and m.IdMarca=pr.IdMarca and p.IdUsuario=@IdUsuario and p.IdPedido=@IdPedido
                        group by p.IdPedido, p.FechaPedido, d.Descripcion,m.Descripcion, d.Cantidad, d.Precio";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
            cmd.Parameters.AddWithValue("@IdUsuario", id);
            cmd.Parameters.AddWithValue("@IdPedido", idPedido);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTODetallePed p = new DTODetallePed();
                    p.IdPedido = (int)dr["idPedido"];
                    p.Fecha = (DateTime)dr["FechaPedido"];
                    p.Producto = dr["Descripcion"].ToString();
                    p.Marca = dr["Marca"].ToString();
                    p.Cantidad = (int)dr["Cantidad"];
                    p.Precio = (decimal)dr["Precio"];


                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }

        public static List<DTOUsuarioTotal> GetListaPedidosDespachadosXCliente()
        {
            var lista = new List<DTOUsuarioTotal>();
            var sql = @"select p.IdUsuario,u.Apellido + ' ' + u.Nombre as Cliente,  sum (d.Precio*d.Cantidad) as  'TotalPedido'
                            from Pedidoes p, DetallePedidoes d, Usuarios u, Estadoes e
                            where p.IdPedido=d.IdPedido and p.IdUsuario=u.idUsuario and p.IdEstado=e.IdEstado
                            and MONTH(FechaPedido)= MONTH(getdate())
                            and p.IdEstado=3
                            group by p.IdUsuario, u.Apellido + ' ' + u.Nombre
";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOUsuarioTotal u = new DTOUsuarioTotal();

                    u.IdUsuario=(int)dr["IdUsuario"];
                    u.Cliente = dr["Cliente"].ToString();
                    u.TotalPedido = (decimal)dr["TotalPedido"];

                    lista.Add(u);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }

        public static List<DTOPed2> GetListaPedidos()
        {


            var lista = new List<DTOPed2>();
            var sql = @"select FechaPedido,  p.IdPedido, u.Apellido + ' '+ u.Nombre as Usuario, e.Descripcion,  sum(d.Precio*d.Cantidad) Total
                            from Pedidoes p, Usuarios u, Estadoes e, DetallePedidoes d
                            where p.IdUsuario=u.idUsuario and d.IdPedido=p.IdPedido and e.IdEstado=p.IdEstado and
                            MONTH(FechaPedido)= MONTH(getdate())
                            group by FechaPedido, p.IdPedido, u.Apellido, u.Nombre, e.Descripcion";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
      
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOPed2 p = new DTOPed2();
       
                    p.Fecha = (DateTime)dr["FechaPedido"];
                    p.IdPedido = (int)dr["IdPedido"];
                    p.Usuario = dr["Usuario"].ToString();
                    p.Estado = dr["Descripcion"].ToString();
                    p.Total = (decimal)dr["Total"];

                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }

        public static List<DTOPorcentajes> GetListaPedidosYPorcentajes()
        {
            var lista = new List<DTOPorcentajes>();
            var sql = @"select p.IdEstado, e.Descripcion as Estado,  count(*) * 100.0 / sum(count(*)) over() as Porcentaje
                            from Pedidoes p, Estadoes e
                            where  MONTH(FechaPedido)= MONTH(getdate()) and p.IdEstado=e.IdEstado
                            group by p.IdEstado, e.Descripcion";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOPorcentajes u = new DTOPorcentajes();


                    u.IdEstado = (int)dr["IdEstado"];
                    u.Estado = dr["Estado"].ToString();
                    u.Porcentaje = (decimal)dr["Porcentaje"];
                    lista.Add(u);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }

        public static List<DTOPed2> GetListaPedidosXEstados(int id)
        {


            var lista = new List<DTOPed2>();
            var sql = @"select FechaPedido,  p.IdPedido, u.Apellido + ' '+ u.Nombre as Usuario, e.Descripcion,  sum(d.Precio*d.Cantidad) Total
                            from Pedidoes p, Usuarios u, Estadoes e, DetallePedidoes d
                            where p.IdUsuario=u.idUsuario and d.IdPedido=p.IdPedido and e.IdEstado=p.IdEstado and p.IdEstado=@IdEstado and
                            MONTH(FechaPedido)= MONTH(getdate())
                            group by FechaPedido, p.IdPedido, u.Apellido, u.Nombre, e.Descripcion";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
            cmd.Parameters.AddWithValue("@IdEstado", id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOPed2 p = new DTOPed2();

                    p.Fecha = (DateTime)dr["FechaPedido"];
                    p.IdPedido = (int)dr["IdPedido"];
                    p.Usuario = dr["Usuario"].ToString();
                    p.Estado = dr["Descripcion"].ToString();
                    p.Total = (decimal)dr["Total"];

                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }
        public static List<Estado> GetListaEstados()
        {


            var lista = new List<Estado>();
            var sql = @"select IdEstado, Descripcion
                            from Estadoes";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
       
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    Estado e = new Estado();

                    
                    e.IdEstado = (int)dr["IdEstado"];
                    e.Descripcion = dr["Descripcion"].ToString();
                  
                    lista.Add(e);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }
        public static List<DTOClientePedido> GetClientesXPedidos(int id)
        {


            var lista = new List<DTOClientePedido>();
            var sql = @"select p.IdUsuario,p.IdPedido, p.FechaPedido, d.Cantidad, d.Precio, Cantidad*Precio as Total, e.Descripcion
                        from Pedidoes p, Usuarios u, DetallePedidoes d, Estadoes e
                        where p.IdPedido=d.IdPedido and u.idUsuario= p.IdUsuario and p.IdEstado=e.IdEstado
                        and p.IdUsuario=@IdUsuario and MONTH(p.FechaPedido)= MONTH(getdate())
                        group by p.IdUsuario, p.IdPedido, p.FechaPedido, d.Descripcion, d.Cantidad, d.Precio, e.Descripcion";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
            cmd.Parameters.AddWithValue("@IdUsuario", id);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOClientePedido p = new DTOClientePedido();
                    p.IdUsuario = (int)dr["IdUsuario"];
                    p.IdPedido = (int)dr["idPedido"];
                    p.Fecha = (DateTime)dr["FechaPedido"];
                    p.Cantidad = (int)dr["Cantidad"];
                    p.Precio = (decimal)dr["Precio"];
                    p.Total = (decimal)dr["Total"];
                    p.Estado = dr["Descripcion"].ToString();
                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }
        public static List<DTOUsuario> GetListaUsuarios()
        {


            var lista = new List<DTOUsuario>();
            var sql = @"  select IdUsuario,  Apellido + ' ' + Nombre + ' / ' + UserName as Nombre
                            from Usuarios";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOUsuario u = new DTOUsuario();


                    u.IdUsuario = (int)dr["IdUsuario"];
                   
                    u.Nombre = dr["Nombre"].ToString();
                    lista.Add(u);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }

        public static List<DTOUsuarioTotal> GetListaClientesYTotales()
        {
            var lista = new List<DTOUsuarioTotal>();
            var sql = @"select p.IdUsuario,u.Apellido + ' ' + u.Nombre as Cliente,  sum (d.Precio*d.Cantidad) as  'TotalPedido'
                            from Pedidoes p, DetallePedidoes d, Usuarios u, Estadoes e
                            where p.IdPedido=d.IdPedido and p.IdUsuario=u.idUsuario and p.IdEstado=e.IdEstado
                            and MONTH(FechaPedido)= MONTH(getdate())
                            group by p.IdUsuario, u.Apellido + ' ' + u.Nombre";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOUsuarioTotal u = new DTOUsuarioTotal();


                    u.IdUsuario = (int)dr["IdUsuario"];
                    u.Cliente = dr["Cliente"].ToString();
                    u.TotalPedido = (decimal)dr["TotalPedido"];
                    lista.Add(u);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }
        public static List<DTOProductoCombo> GetListaProductosComboAgregar()
        {
            var lista = new List<DTOProductoCombo>();
            var sql = @"select p.IdProducto, p.Descripcion + ' / ' + m.Descripcion + ' / $ ' + cast( p.Precio as varchar) as Producto
                        from Productoes p, Marcas m
                        where p.IdMarca=m.IdMarca";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOProductoCombo p = new DTOProductoCombo();

                    p.IdProducto = (int)dr["IdProducto"];
                    p.Producto = dr["Producto"].ToString();
                  

                    lista.Add(p);
                    
                }
            }
            dr.Close();
            conex.Close();
            lista.Add(new DTOProductoCombo
            {
                IdProducto = 0,
                Producto = "[Seleccione una Producto...]"
            });
            return lista.OrderBy(x => x.Producto).ToList();
         
        }
    }
}