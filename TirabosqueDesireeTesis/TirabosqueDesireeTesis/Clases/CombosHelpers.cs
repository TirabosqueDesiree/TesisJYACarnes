using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Clases
{
    public class CombosHelpers: IDisposable
    {
        private static CarniceriaContext db = new CarniceriaContext();

        public static List<Provincia> GetProvincias()
        {
            var provincias = db.Provincias.ToList();
            provincias.Add(new Provincia
            {
                IdProvincia = 0,
                Descripcion = "[Seleccione una Provincia...]"
            });
            return provincias.OrderBy(x => x.Descripcion).ToList();

        }
        public static List<Producto> GetProductos()
        {
            var productos = db.Productoes.ToList();
            productos.Add(new Producto
            {
                IdProducto = 0,
                Descripcion = "[Seleccione una Producto...]"
            });
            return productos.OrderBy(x => x.Descripcion).ToList();

        }
        public static List<Usuario> GetUsuarios()
        {
            var usuarios = db.Usuarios.ToList();
            usuarios.Add(new Usuario
            {
                IdUsuario = 0,
                UserName = "[Seleccione un Usuario...]"
            });
            return usuarios.OrderBy(x => x.UserName).ToList();

        }
        public static List<Localidad> GetLocalidades()
        {
            var localidades = db.Localidads.ToList();
            localidades.Add(new Localidad
            {
                IdProvincia = 0,
                Descripcion = "[Seleccione una Localidad...]"
            });
            return localidades.OrderBy(x => x.Descripcion).ToList();

        }

        public void Dispose()
        {

            db.Dispose();
        }
        public static List<Categoria> GetCategorias()
        {
            var categorias = db.Categorias.ToList();
            categorias.Add(new Categoria
            {
                IdCategoria = 0,
                Descripcion = "[Seleccione una Categoria...]"
            });
            return categorias.OrderBy(x => x.Descripcion).ToList();

        }
        public static List<Marca> GetMarcas()
        {
            var marcas = db.Marcas.ToList();
            marcas.Add(new Marca
            {
                IdMarca = 0,
                Descripcion = "[Seleccione una Marca...]"
            });
            return marcas.OrderBy(x => x.Descripcion).ToList();

        }

    }
}
