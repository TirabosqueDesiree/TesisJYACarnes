using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Clases
{
    public class ReportesHelpers
    {
        private static CarniceriaContext db = new CarniceriaContext();
        private static string cadenaCon = "Data Source=desiree;Initial Catalog=DesireeCarniceria;Integrated Security=True";


        public static List<DTOPed2> GetPedidosdespachados(DateTime fechaUno, DateTime fechaDos)
        {
            string cadenaCon = "Data Source=desiree;Initial Catalog=DesireeCarniceria;Integrated Security=True";

            var lista = new List<DTOPed2>();
            var sql = @" select p.IdPedido, p.FechaPedido, u.Apellido + ' ' + Nombre as Usuario, e.Descripcion, sum(d.Cantidad* d.Precio) as Total
                          from Pedidoes p, Estadoes e, Usuarios u, DetallePedidoes d
                          where p.IdEstado=e.IdEstado and u.idUsuario=p.IdUsuario and d.IdPedido=p.IdPedido and
                          e.Descripcion like '%Despachado%'
                          and p.FechaPedido  between  @fechaUno and @fechaDos
                          group by  p.IdPedido, p.FechaPedido, u.Apellido + ' ' + Nombre, e.Descripcion";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
            cmd.Parameters.AddWithValue("@fechaUno", fechaUno);
            cmd.Parameters.AddWithValue("@fechaDos", fechaUno);
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
        public static List<DTOPed2> GetTodosPedidosdespachados()
        {
            string cadenaCon = "Data Source=desiree;Initial Catalog=DesireeCarniceria;Integrated Security=True";

            var lista = new List<DTOPed2>();
            var sql = @"select p.IdPedido, p.FechaPedido, u.Apellido + ' ' + Nombre as Usuario, e.Descripcion, sum(d.Cantidad* d.Precio) as Total
                              from Pedidoes p, Estadoes e, Usuarios u, DetallePedidoes d
                              where p.IdEstado=e.IdEstado and u.idUsuario=p.IdUsuario and d.IdPedido=p.IdPedido and
                              e.Descripcion like '%Despachado%'
                              group by  p.IdPedido, p.FechaPedido, u.Apellido + ' ' + Nombre, e.Descripcion";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
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
        

        public static List<Estado> GetEstados()
        {
            var estados = db.Estadoes.ToList();
            estados.Add(new Estado
            {
                IdEstado = 0,
                Descripcion = "[Seleccione un Estado...]"
            });
            return estados.OrderBy(x => x.Descripcion).ToList();

        }

    }
}