using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Clases
{
    public class ListadosProdHelpers
    {
        private static string cadenaCon = "Data Source=desiree;Initial Catalog=DesireeCarniceria;Integrated Security=True";

        public static List<DTOProdListado> GetListadoPollos()
        {


            var lista = new List<DTOProdListado>();
            var sql = @"SELECT p.Descripcion as Producto, m.Descripcion as Marca, p.Precio, p.Imagen
                        from Productoes p, Categorias c, Marcas m
                        where p.IdCategoria=c.IdCategoria and p.IdMarca=m.IdMarca 
                        and c.IdCategoria=1";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);
           
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOProdListado p = new DTOProdListado();
                    
                    p.Descripcion = dr["Producto"].ToString();
                    p.Marca = dr["Marca"].ToString();
                    p.Precio = (decimal)dr["Precio"];
                    p.Imagen = dr["Imagen"].ToString();

                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }
        public static List<DTOProdListado> GetListadoPescados()
        {


            var lista = new List<DTOProdListado>();
            var sql = @"SELECT p.Descripcion as Producto, m.Descripcion as Marca, p.Precio, p.Imagen
                        from Productoes p, Categorias c, Marcas m
                        where p.IdCategoria=c.IdCategoria and p.IdMarca=m.IdMarca 
                        and c.IdCategoria=2";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOProdListado p = new DTOProdListado();

                    p.Descripcion = dr["Producto"].ToString();
                    p.Marca = dr["Marca"].ToString();
                    p.Precio = (decimal)dr["Precio"];
                    p.Imagen = dr["Imagen"].ToString();

                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }
        public static List<DTOProdListado> GetListadoCarneCerdo()
        {


            var lista = new List<DTOProdListado>();
            var sql = @"SELECT p.Descripcion as Producto, m.Descripcion as Marca, p.Precio, p.Imagen
                        from Productoes p, Categorias c, Marcas m
                        where p.IdCategoria=c.IdCategoria and p.IdMarca=m.IdMarca 
                        and c.IdCategoria=3";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOProdListado p = new DTOProdListado();

                    p.Descripcion = dr["Producto"].ToString();
                    p.Marca = dr["Marca"].ToString();
                    p.Precio = (decimal)dr["Precio"];
                    p.Imagen = dr["Imagen"].ToString();

                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }
        public static List<DTOProdListado> GetListadoCarneVacuna()
        {


            var lista = new List<DTOProdListado>();
            var sql = @"SELECT p.Descripcion as Producto, m.Descripcion as Marca, p.Precio, p.Imagen
                        from Productoes p, Categorias c, Marcas m
                        where p.IdCategoria=c.IdCategoria and p.IdMarca=m.IdMarca 
                        and c.IdCategoria=4";

            SqlConnection conex = new SqlConnection(cadenaCon);
            conex.Open();
            SqlCommand cmd = new SqlCommand(sql, conex);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    DTOProdListado p = new DTOProdListado();

                    p.Descripcion = dr["Producto"].ToString();
                    p.Marca = dr["Marca"].ToString();
                    p.Precio = (decimal)dr["Precio"];
                    p.Imagen = dr["Imagen"].ToString();

                    lista.Add(p);
                }
            }
            dr.Close();
            conex.Close();

            return lista;
        }
    }
}